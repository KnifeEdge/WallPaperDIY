#简介




#实现原理

原文链接：https://www.codeproject.com/Articles/856020/Draw-Behind-Desktop-Icons-in-Windows-plus 

中文译文：https://blog.csdn.net/qilei2010/article/details/117623657 

windows的窗体结构可以在spy++软件上查看，该软件是在visual studio顶部的工具菜单里面可以找到。在窗口有列表的最后有三个窗口，分别是两个WorkerW和一个progman。
第一个WorkerW里面是桌面图标所在的层，里面包含一个窗口:SysListView32；
第二个WorkerW是桌面背景所在的图层。
第三个progman是最后一个图层，是shell所在的层；

有两种方案，第一种是在第二个WorkerW中显示我们自己的背景图像；另一种是将第二个WorkerW隐藏，在progman中显示背景。第一种方法在win10上测试成功，具体实现方案见上面的链接。

本项目是使用winform内嵌chrome内核实现的html网页桌面背景，目前未实现与背景层的交互，当时原理上是可行的，只是比较麻烦。
winform是用c#写的，而开发运行winform应用需要.NET环境支持，你的电脑上需要先安装.NET框架才能正常开发和使用winform应用。
本项目使用mono实现了脱离.NET的应用开发，客户端无需安装.NET框架即可正常运行该软件。

mono官网：https://www.mono-project.com/

CefSharp 是一个c#运行chrome内核的第三方库，可以在官网上自行下载使用，


#编译

为了实现脱离.NET的应用开发，首先要现在安装mono软件，官网上有教程。默认安装位置为 C:\Program Files\Mono ，里面主要是 bin 和 lib 这两个文件夹。
在mono文件夹下新建一个文件夹MyProgram,将本项目的cs文件和resources文件夹复制到MyProgram中。将Cefsharp和其他必要的依赖库放在MyProgram文件夹中，我的文件列表如下图所示：

在Mono文件中打开cmd窗口，运行命令：

`bin\csc -target:winexe /r:System.Data.dll /r:System.Drawing.dll /r:System.Windows.Forms.dll /r:MyProgram\CefSharp.BrowserSubprocess.Core.dll /r:MyProgram\CefSharp.Core.dll /r:MyProgram\CefSharp.dll /r:MyProgram\CefSharp.WinForms.dll test\*.cs`

这里可能会报错说找不到某个模块，比如在System里找不到Data模块，那就在命令里添加`/r:System.Data.dll`如上所示。

如果显示如下就是编译成功了，编译成功的exe文件将出现在Mono文件夹中。

`Microsoft (R) Visual C# Compiler version 3.6.0-4.20224.5 (ec77c100)
Copyright (C) Microsoft Corporation. All rights reserved.`


#使用方式






