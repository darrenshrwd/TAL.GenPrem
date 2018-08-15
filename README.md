## Areas of improvement and refinement if you had a full 2 days to build this application

1. I would split the UnitTest.cs file up for the relevant classes being tested.
2. So far I have only written a single test for exception handling etc, otherwise I have only tested 'positive' outcomes.
3. I would add a couple of tests around the age calc for leap years.
4. I have not added any front end tests - personally I would probably need more than 2 days to do that right.

5. Obviously there are a bunch of magic numbers that would come from some kind of database in real life.
6. There is a bunch of css for carousel, etc I could chop out that came from template.

-----------------------------------------------------------------
I like ServiceStack but had only used the full framework version, so I started with their .NET CORE MVC template...
-----------------------------------------------------------------
# mvc

.NET Core 2.1 MVC Website with ServiceStack APIs

> Browse [source code](https://github.com/NetCoreTemplates/mvc), view live demo [mvc.web-templates.io](http://mvc.web-templates.io) and install with [dotnet-new](http://docs.servicestack.net/dotnet-new):

    $ npm install -g @servicestack/cli

    $ dotnet-new mvc ProjectName

