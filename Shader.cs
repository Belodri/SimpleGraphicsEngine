using System.Reflection;
using OpenTK.Graphics.OpenGL4;

namespace SimpleGraphicsEngine;

public class Shader {
    public readonly int Handle;

    public static string GetShaderFilePath(string fileName) {
        string root = AppDomain.CurrentDomain.BaseDirectory;
        return Path.Combine(root, @"shaders\", fileName);
    }

    public Shader(string vertexFileName, string fragmentFileName) {
        int VertexShader, FragmentShader;

        string VertextShaderSource = File.ReadAllText(GetShaderFilePath(vertexFileName));
        string FragmentShaderSource = File.ReadAllText(GetShaderFilePath(fragmentFileName));

        VertexShader = GL.CreateShader(ShaderType.VertexShader);
        FragmentShader = GL.CreateShader(ShaderType.FragmentShader);

        GL.ShaderSource(VertexShader, VertextShaderSource);
        GL.ShaderSource(FragmentShader, FragmentShaderSource);

        GL.CompileShader(VertexShader);
        GL.GetShader(VertexShader, ShaderParameter.CompileStatus, out int vertexSuccess);
        if(vertexSuccess == 0) {
            string infoLog = GL.GetShaderInfoLog(VertexShader);
            Console.WriteLine(infoLog);
        }

        GL.CompileShader(FragmentShader);
        GL.GetShader(FragmentShader, ShaderParameter.CompileStatus, out int fragmentSuccess);
        if(fragmentSuccess == 0) {
            string infoLog = GL.GetShaderInfoLog(FragmentShader);
            Console.WriteLine(infoLog);
        }

        Handle = GL.CreateProgram();

        GL.AttachShader(Handle, VertexShader);
        GL.AttachShader(Handle, FragmentShader);

        GL.LinkProgram(Handle);

        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkSuccess);
        if(linkSuccess == 0) {
            string infoLog = GL.GetProgramInfoLog(Handle);
            Console.WriteLine(infoLog);
        }

        //cleanup
        GL.DetachShader(Handle, VertexShader);
        GL.DetachShader(Handle, FragmentShader);
        
        GL.DeleteShader(VertexShader);
        GL.DeleteShader(FragmentShader);
    }

    public void Use() {
        GL.UseProgram(Handle);
    }
}