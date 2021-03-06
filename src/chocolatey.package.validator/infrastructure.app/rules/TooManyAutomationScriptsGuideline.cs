﻿// Copyright © 2015 - Present RealDimensions Software, LLC
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
//
// You may obtain a copy of the License at
//
// 	http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

namespace chocolatey.package.validator.infrastructure.app.rules
{
    using System.IO;
    using System.Linq;
    using NuGet;
    using infrastructure.rules;

    public class TooManyAutomationScriptsGuideline : BasePackageRule
    {
        public override string ValidationFailureMessage { get { return 
@"There are more than 3 automation scripts in this package. This is not recommended as it increases the complexity of the package. [More...](https://github.com/chocolatey/package-validator/wiki/MoreThanMaximumAutomationScripts)"; } }

        public override PackageValidationOutput is_valid(IPackage package)
        {
            var valid = true;

            var files = package.GetFiles().or_empty_list_if_null();

            var numberOfInstallationScripts = 0;

            foreach (var packageFile in files)
            {
                string extension = Path.GetExtension(packageFile.Path).to_lower();
                if (extension != ".ps1" && extension != ".psm1") continue;

                numberOfInstallationScripts += 1;
            }

            return numberOfInstallationScripts <= 3;
        }
    }
}
