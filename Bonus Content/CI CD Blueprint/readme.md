The CI/CD Blueprint contains a pipeline workflow file to build, test, and publish your .NET projects. It's meant to serve as a baseline for your CI pipelines. The only remaining thing is adding a deployment target, such as deploying to Azure or AWS.

I created separate YAML files for GitHub Actions and Azure DevOps.

The workflow contains:

Environment variables for configuring the DOTNET_VERSION and SOLUTION_PATH, allowing you to update this for your project easily.
The pipeline has four steps:
Restore
Build - with --no-restore to improve performance
Test - with --no-restore --no-build to improve performance
Publish - with --no-restore --no-build to improve performance