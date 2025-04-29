The Code Quality Shortcut will help you set up code quality and code style rules for your entire solution.

There are two files you need to add to your solution:

Directory.Build.props
.editorconfig
The .editorconfig allows you to configure the default code style rules you want to apply to your C# code. I've included some sensible default values. However, you're welcome to customize the .editorconfig file to fit your needs.

The Directory.Build.props file allows you to define default dependencies for all your projects. I've configured it to treat compiler warnings as errors (so you'll have to fix them) and to install the SonarAnalyzer.CSharp library that introduces additional source code analyzers.