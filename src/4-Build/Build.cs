// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

using CodeBlock.DevKit.Build;

namespace Build;

public class Build : BaseBuild
{
    public static int Main() => Execute<Build>(x => x.RunUnitTests);
}
