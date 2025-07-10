// Copyright (c) CodeBlock.Dev. All rights reserved.
// For more information visit https://codeblock.dev

namespace CanBeYours.Api;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.ConfigureServices().ConfigurePipeline();

        app.Run();
    }
}
