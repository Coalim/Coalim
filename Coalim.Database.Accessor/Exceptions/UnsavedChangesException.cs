namespace Coalim.Database.Accessor.Exceptions;

public class UnsavedChangesException() : Exception("The database had unsaved changes when attempting to dispose it.");