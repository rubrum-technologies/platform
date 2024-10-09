using FluentValidation;
using HotChocolate;
using MediatR;
using Volo.Abp.Guids;

namespace Rubrum.Platform.DataSourceService.Database.Commands;

[GraphQLName("CreateDatabaseSourceInput")]
public class CreateDatabaseSourceCommand : IRequest<DatabaseSource>
{
    public DatabaseKind Kind { get; init; }

    public required string Name { get; init; }

    public required string Prefix { get; init; }

    public required string ConnectionString { get; init; }

    public required IReadOnlyList<CreateDatabaseTableInput> Tables { get; init; }

    public sealed record CreateDatabaseTableInput(
        string Name,
        string SystemName,
        IEnumerable<CreateDatabaseColumnInput> Columns);

    public sealed record CreateDatabaseColumnInput(
        DataSourceEntityPropertyKind Kind,
        string Name,
        string SystemName);

    public class Handler(
        IGuidGenerator guidGenerator,
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
                request.Prefix,
                request.ConnectionString,
                request.Tables.Select(t => new CreateDatabaseTable(
                    guidGenerator.Create(),
                    t.Name,
                    t.SystemName,
                    t.Columns.Select(c => new CreateDatabaseColumn(
                        guidGenerator.Create(),
                        c.Kind,
                        c.Name,
                        c.SystemName)))));

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

            RuleFor(x => x.Prefix)
                .MaximumLength(DataSourceConstants.PrefixLength)
                .NotEmpty();

            RuleFor(x => x.ConnectionString)
                .NotEmpty();

            RuleFor(x => x.Tables)
                .NotEmpty();

            RuleForEach(x => x.Tables)
                .NotNull()
                .SetValidator(new CreateDatabaseTableInputValidator());
        }

        public class CreateDatabaseTableInputValidator : AbstractValidator<CreateDatabaseTableInput>
        {
            public CreateDatabaseTableInputValidator()
            {
                RuleFor(x => x.Name)
                    .MaximumLength(DatabaseTableConstants.NameLength)
                    .NotEmpty();

                RuleFor(x => x.SystemName)
                    .MaximumLength(DatabaseTableConstants.SystemNameLength)
                    .NotEmpty();

                RuleForEach(x => x.Columns)
                    .NotEmpty()
                    .SetValidator(new CreateDatabaseColumnInputValidator());
            }
        }

        public class CreateDatabaseColumnInputValidator : AbstractValidator<CreateDatabaseColumnInput>
        {
            public CreateDatabaseColumnInputValidator()
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
