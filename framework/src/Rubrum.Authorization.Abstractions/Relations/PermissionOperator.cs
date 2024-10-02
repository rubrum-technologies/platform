namespace Rubrum.Authorization.Relations;

public enum PermissionOperator
{
    /// <summary>
    /// + (Union)
    /// Unions together the relations/permissions referenced
    /// Union is the most common operation and is used to join different relations or permissions together to form a set of allowed subjects.
    /// </summary>
    Union,

    /// <summary>
    /// &#38; (Intersection)
    /// Intersects the set of subjects found for the relations/permissions referenced
    /// Intersection allows for a permission to only include those subjects that were found in both relations/permissions.
    /// </summary>
    Intersection,

    /// <summary>
    /// - (Exclusion)
    /// Excludes the set of subjects found for the right side relation/permission from those found the left side relation/permission
    /// Exclusion allows for computing the difference between two sets of relations/permissions.
    /// </summary>
    Exclusion,

    /// <summary>
    /// -> (Arrow)
    /// Arrows allow for "walking" the heirarchy of relations (and permissions) defined for an object of a subject,
    /// referencing a permission or relation on the resulting subject's object.
    /// </summary>
    Arrow,
}
