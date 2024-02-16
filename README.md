Sample minimal ASP.NET core API to test dynamic reflection with preloaded Testable instances and posted assemblies containing these.

Assemblies can be created with the <b>Testable</b> package (available from nuget.org) and one or more implementations of the TestableBase class. These can then be tested by posting to the API using https://localhost:7286/UploadTestable (assuming default port and running locally) and the dll file within the form-data.
