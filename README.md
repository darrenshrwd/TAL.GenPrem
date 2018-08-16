N.B: To run this you will need the very latest .NET Core SDK (2.1.400)
https://www.microsoft.com/net/download/dotnet-core/2.1

This is due to a dependency on ASP.NET Core Runtime 2.1.2.

I like ServiceStack but had only used the full framework version, so I started with their .NET CORE MVC template...

But then I removed all the MVC stuff anyway and totally hacked in an Vue.js example form:
https://auralinna.blog/post/2018/how-to-build-a-complete-form-with-vue-js
https://github.com/teroauralinna/vue-demo-form

Copying and modifying the webpack config, so it could be called when run project via: 
https://docs.microsoft.com/en-us/aspnet/core/client-side/spa-services?view=aspnetcore-2.1

I got npm install to work under node 8.11.3, running command prompt as administrator.

## Areas of improvement if you had a full 2 days to build this application

1. I would split the UnitTest.cs file up for the relevant classes being tested.
2. So far I have only written a single test for exception handling etc, otherwise I have only tested 'positive' outcomes.
3. I would add a couple of tests around the age calc for leap years.
4. I have not added any front end tests - personally I would probably need more than 2 days to do that right.

5. Obviously there are a bunch of magic numbers that would come from some kind of database in real life.
6. The frontend vue form component could be broken up a bit to smaller components.
7. I hacked in a webpack build that I have ZERO experience with* or any real idea of how it works...I would try and learn it. 
   I am also unsure if a prod build would work currently. The 'modern' frontend in general was a learning exercise and the node packages used have security warnings.
   
   *The last time I did a front end build I used Gulp+JSPM, took me ages to get to work, but works well.
   
   Really I should have avoided a FE build for this exercise, especially as Vue can work without any.