Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Http
Imports Microsoft.Extensions.Hosting

Module Program
    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)
        
        Dim app = builder.Build()
        
        ' Home page with input form
        app.MapGet("/", Async Function(context As HttpContext)
                            Dim html = "<!DOCTYPE html>
<html>
<head>
    <title>WebGoat VB - User Input Demo</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #f5f5f5;
        }
        .container {
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }
        h1 {
            color: #333;
        }
        form {
            margin-top: 20px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            color: #555;
        }
        input[type='text'] {
            width: 100%;
            padding: 10px;
            margin-bottom: 15px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        input[type='submit'] {
            background-color: #4CAF50;
            color: white;
            padding: 12px 30px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            font-size: 16px;
        }
        input[type='submit']:hover {
            background-color: #45a049;
        }
    </style>
</head>
<body>
    <div class='container'>
        <h1>WebGoat VB - User Input Demo</h1>
        <p>Enter your information below and submit to see it rendered:</p>
        <form action='/submit' method='post'>
            <label for='username'>Username:</label>
            <input type='text' id='username' name='username' required>
            
            <label for='message'>Message:</label>
            <input type='text' id='message' name='message' required>
            
            <input type='submit' value='Submit'>
        </form>
    </div>
</body>
</html>"
                            context.Response.ContentType = "text/html"
                            Await context.Response.WriteAsync(html)
                        End Function)
        
        ' Handle form submission
        app.MapPost("/submit", Async Function(context As HttpContext)
                                   Dim form = Await context.Request.ReadFormAsync()
                                   Dim username = form("username").ToString()
                                   Dim message = form("message").ToString()
                                   
                                   Dim html = $"<!DOCTYPE html>
<html>
<head>
    <title>WebGoat VB - Submission Result</title>
    <style>
        body {{
            font-family: Arial, sans-serif;
            max-width: 800px;
            margin: 50px auto;
            padding: 20px;
            background-color: #f5f5f5;
        }}
        .container {{
            background-color: white;
            padding: 30px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0,0,0,0.1);
        }}
        h1 {{
            color: #333;
        }}
        .result {{
            background-color: #e8f5e9;
            padding: 20px;
            border-radius: 4px;
            margin: 20px 0;
            border-left: 4px solid #4CAF50;
        }}
        .label {{
            font-weight: bold;
            color: #555;
        }}
        .value {{
            color: #333;
            margin-left: 10px;
        }}
        a {{
            display: inline-block;
            margin-top: 20px;
            color: #4CAF50;
            text-decoration: none;
            font-weight: bold;
        }}
        a:hover {{
            text-decoration: underline;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <h1>Submission Received!</h1>
        <div class='result'>
            <p><span class='label'>Username:</span><span class='value'>{username}</span></p>
            <p><span class='label'>Message:</span><span class='value'>{message}</span></p>
        </div>
        <a href='/'>← Go Back</a>
    </div>
</body>
</html>"
                                   context.Response.ContentType = "text/html"
                                   Await context.Response.WriteAsync(html)
                               End Function)
        
        app.Run()
    End Sub
End Module
