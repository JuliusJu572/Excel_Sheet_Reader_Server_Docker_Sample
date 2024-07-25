# Excel_Sheet_Reader_Server_Docker_Sample
该仓库包含一个Docker化的应用程序，用于读取Excel文件中的Sheet名称并通过HTTP服务器提供服务。
This repository contains a Dockerized application that reads the sheet names from an Excel file and serves them through an HTTP server. 

# 部署一个 C# 程序：读取某个excel中的所有sheet名称，并发送给服务器（中文）

**重要参考网站**：[Connection refused? Docker networking and how it impacts your image](https://pythonspeed.com/articles/docker-connection-refused/)

## C# 程序的作用

**示例文件作用**

1. **监听端口**：程序启动后会监听指定的URL（本地服务器的特定端口），等待接收HTTP POST请求。
2. **接收请求**：当接收到POST请求并且请求包含实体内容时，程序会读取请求体中的Excel文件名。
3. **读取Excel文件**：程序检查该文件是否存在，如果存在，则使用OfficeOpenXml库打开该Excel文件。
4. **获取Sheet名称**：程序会获取Excel文件中的所有Sheet名称。
5. **返回JSON**：程序会将获取到的Sheet名称以JSON格式返回给客户端。如果文件不存在或请求无效，则返回错误消息。

## Flask 程序的作用

**示例文件作用**

1. **创建Flask应用**：程序创建一个Flask应用，并定义一个接收POST请求的路由。
2. **接收请求**：当接收到POST请求时，程序会从请求体中获取JSON数据。
3. **打印内容**：程序会将接收到的内容打印到控制台，以便进行调试和确认。
4. **返回确认消息**：程序会返回一个JSON格式的确认消息给客户端，表示数据已成功接收。

**部署docker**

（C#程序中）右键解决方案 -> 添加 -> docker 支持 -> Linux

在生成的Dockerfile中添加，添加如下代码

```C#
# 暴露应用程序使用的端口
EXPOSE 5000

# 运行应用程序
ENTRYPOINT ["dotnet", "ExcelReader.dll"]
```

在cmd中，cd到文件目录输入如下代码，实现文件发布（release）

```sh
dotnet build -c Release
```

生成docker 镜像

这里可能需要多次运行（网络问题）

```sh
docker build -t excelreaderimage .
```

运行Docker容器

```sh
docker run -d -p 5000:5000 -v D:/ExcelReader/ExcelReader/file.xlsx:/app/files/file.xlsx --name excelreader_container excelreaderimage
```

### 详细解释

1. **docker run**: 这是Docker命令，用于运行一个新的容器实例。
    
2. **-d**: 以分离（detached）模式运行容器。容器将在后台运行。
    
3. **-p 5000:5000**: 将主机的5000端口映射到容器的5000端口。这意味着访问主机的`http://localhost:5000`将被转发到容器的5000端口。
    
4. **-v D:/ExcelReader/ExcelReader/file.xlsx:/app/files/file.xlsx**: 将主机上的文件`D:/ExcelReader/ExcelReader/file.xlsx`挂载到容器内的`/app/files/file.xlsx`路径。这允许容器访问主机上的文件。
    
5. **--name excelreader_container**: 给容器命名为`excelreader_container`。这使得以后可以通过这个名称来引用和管理该容器。
    
6. **excelreaderimage**: 使用名称为`excelreaderimage`的Docker镜像来创建容器。


运行即可传输成功。


# Deploy a C# Program: Read All Sheet Names from an Excel File and Send Them to a Server

**Important Reference Website**: Connection refused? Docker networking and how it impacts your image

## C# Program Description

**Example File Functionality**

1. **Listen on Port**: The program starts and listens on a specified URL (local server's specific port), waiting for HTTP POST requests.
2. **Receive Request**: When a POST request is received and contains an entity body, the program reads the Excel file name from the request body.
3. **Read Excel File**: The program checks if the file exists. If it does, it uses the OfficeOpenXml library to open the Excel file.
4. **Get Sheet Names**: The program retrieves all sheet names from the Excel file.
5. **Return JSON**: The program returns the retrieved sheet names in JSON format to the client. If the file does not exist or the request is invalid, it returns an error message.

## Flask Program Description

**Example File Functionality**

1. **Create Flask Application**: The program creates a Flask application and defines a route to receive POST requests.
2. **Receive Request**: When a POST request is received, the program retrieves JSON data from the request body.
3. **Print Content**: The program prints the received content to the console for debugging and confirmation.
4. **Return Confirmation Message**: The program returns a JSON-formatted confirmation message to the client, indicating that the data has been successfully received.

## Deploy Docker

(In the C# program) Right-click on the solution -> Add -> Docker Support -> Linux

In the generated Dockerfile, add the following code:

dockerfile


`# Expose the port used by the application EXPOSE 5000  # Run the application ENTRYPOINT ["dotnet", "ExcelReader.dll"]`

In the command line, navigate to the file directory and enter the following code to publish the release:

sh



`dotnet build -c Release`

Generate the Docker image (this may require multiple attempts due to network issues):

sh



`docker build -t excelreaderimage .`

Run the Docker container:

sh



`docker run -d -p 5000:5000 -v D:/ExcelReader/ExcelReader/file.xlsx:/app/files/file.xlsx --name excelreader_container excelreaderimage`

### Detailed Explanation

1. **docker run**: This is the Docker command used to run a new container instance.
    
2. **-d**: Run the container in detached mode. The container will run in the background.
    
3. **-p 5000:5000**: Map port 5000 on the host to port 5000 in the container. This means that accessing `http://localhost:5000` on the host will be forwarded to port 5000 in the container.
    
4. **-v D:/ExcelReader/ExcelReader/file.xlsx:/app/files/file.xlsx**: Mount the file `D:/ExcelReader/ExcelReader/file.xlsx` from the host to `/app/files/file.xlsx` in the container. This allows the container to access the file on the host.
    
5. **--name excelreader_container**: Name the container `excelreader_container`. This allows you to reference and manage the container by this name in the future.
    
6. **excelreaderimage**: Use the Docker image named `excelreaderimage` to create the container.
