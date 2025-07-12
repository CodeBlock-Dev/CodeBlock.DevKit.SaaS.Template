using CodeBlock.DevKit.Build;

namespace Build;

public class Build : BaseBuild
{
    protected override bool SkipLinting => true;

    public static int Main() => Execute<Build>(x => x.RunUnitTests);
}
