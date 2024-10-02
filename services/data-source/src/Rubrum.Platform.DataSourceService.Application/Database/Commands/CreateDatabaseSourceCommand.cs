using FluentValidation;
using HotChocolate;
using MediatR;

namespace Rubrum.Platform.DataSourceService.Database.Commands;

[GraphQLName("CreateDatabaseSourceInput")]
public class CreateDatabaseSourceCommand : IRequest<DatabaseSource>
{
    public DatabaseKind Kind { get; init; }

    public required string Name { get; init; }

    public required string ConnectionString { get; init; }

    public required IReadOnlyList<CreateDatabaseTable> Tables { get; init; }

    public string? Prefix { get; init; }

    public class Handler(
        DatabaseSourceManager manager,
        IDatabaseSourceRepository repository) : IRequestHandler<CreateDatabaseSourceCommand, DatabaseSource>
    {
        public async Task<DatabaseSource> Handle(
            CreateDatabaseSourceCommand request,
            CancellationToken cancellationToken)
        {
            var source = await manager.CreateAsync(
                request.Kind,
                request.Name,
                request.ConnectionString,
                request.Tables,
                request.Prefix);

            await repository.InsertAsync(source, true, cancellationToken);

            return source;
        }
    }

    public class Validator : AbstractValidator<CreateDatabaseSourceCommand>
    {
        public Validator()
        {
            RuleFor(x => x.Kind)
                .NotNull();

            RuleFor(x => x.Name)
                .MaximumLength(DataSourceConstants.NameLength)
                .NotEmpty();

            RuleFor(x => x.ConnectionString)
                .NotEmpty();

            RuleFor(x => x.Tables)
                .NotEmpty();

            RuleForEach(x => x.Tables)
                .NotNull()
                .SetValidator(new CreateDatabaseTableValidator());
        }

        public class CreateDatabaseTableValidator : AbstractValidator<CreateDatabaseTable>
        {
            public CreateDatabaseTableValidator()
            {
                RuleFor(x => x.Name)
                    .MaximumLength(DatabaseTableConstants.NameLength)
                    .NotEmpty();

                RuleFor(x => x.SystemName)
                    .MaximumLength(DatabaseTableConstants.SystemNameLength)
                    .NotEmpty();

                RuleForEach(x => x.Columns)
                    .NotEmpty()
                    .SetValidator(new CreateDatabaseColumnValidator());
            }
        }

        public class CreateDatabaseColumnValidator : AbstractValidator<CreateDatabaseColumn>
        {
            public CreateDatabaseColumnValidator()
            {
                RuleFor(x => x.Kind)
                    .NotNull();

                RuleFor(x => x.Name)
                    .MaximumLength(DatabaseColumnConstants.NameLength)
                    .NotEmpty();

                RuleFor(x => x.SystemName)
                    .MaximumLength(DatabaseColumnConstants.SystemNameLength)
                    .NotEmpty();
            }
        }
    }
}
