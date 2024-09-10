namespace Rubrum.Authorization.Permissions;

public class Relationship(string relation, ResourceReference resource, SubjectReference subject)
{
    public string Relation => relation;

    public ResourceReference Resource => resource;

    public SubjectReference Subject => subject;
}
