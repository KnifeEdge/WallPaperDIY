# 简介




# 实现原理

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


# 编译

为了实现脱离.NET的应用开发，首先要现在安装mono软件，官网上有教程。默认安装位置为 C:\Program Files\Mono ，里面主要是 bin 和 lib 这两个文件夹。
在mono文件夹下新建一个文件夹MyProgram,将本项目的cs文件和resources文件夹复制到MyProgram中。将Cefsharp和其他必要的依赖库放在MyProgram文件夹中，我的文件列表如下图所示：

在Mono文件中打开cmd窗口，运行命令：

`bin\csc -target:winexe /r:System.Data.dll /r:System.Drawing.dll /r:System.Windows.Forms.dll /r:MyProgram\CefSharp.BrowserSubprocess.Core.dll /r:MyProgram\CefSharp.Core.dll /r:MyProgram\CefSharp.dll /r:MyProgram\CefSharp.WinForms.dll MyProgram\*.cs`

这里可能会报错说找不到某个模块，比如在System里找不到Data模块，那就在命令里添加`/r:System.Data.dll`如上所示。

如果显示如下就是编译成功了，编译成功的exe文件将出现在Mono文件夹中。

`Microsoft (R) Visual C# Compiler version 3.6.0-4.20224.5 (ec77c100)`

`Copyright (C) Microsoft Corporation. All rights reserved.`

将exe文件复制到MyProgram中，运行即可（这里编译成功的exe文件可以在编译命令里指定）。

# 代码详解

`Win32Wrapper.cs`该文件是windows API的封装文件，具体来源忘了，想起来再补上。

`Form1.Designer.cs`里面是窗口类Form1的实现。

在这里，首先创建浏览器内核变量`private ChromiumWebBrowser browser = new ChromiumWebBrowser();`

然后，`setBrowerURL(string url)`函数根据url地址把网页渲染到winform窗体中。

在窗口生成时，还应该做一些预处理，包括
1. `resetBrowerPanelSize()`绑定浏览器内核同时设置浏览器面板相对于窗口的大小，位置
2. 设置窗口属性 FormBorderStyle 为 none，去除顶部菜单栏和边框。

`Program.cs`文件是主要的处理流程：
```
IntPtr progman = W32.FindWindow("Progman", null);//拿到progman窗体图层的指针；

//这个函数可以认为是对workerW两个图层的初始化处理
W32.SendMessageTimeout(progman,
                       0x052C,
                       new IntPtr(0),
                       IntPtr.Zero,
                       W32.SendMessageTimeoutFlags.SMTO_NORMAL,
                       1000,
                       out result);
                       
//找到两个WorkerW图层的指针
IntPtr workerw = IntPtr.Zero;//桌面图标图层
IntPtr workerws = IntPtr.Zero;
W32.EnumWindows(new W32.EnumWindowsProc((tophandle, topparamhandle) =>
{
    IntPtr p = W32.FindWindowEx(tophandle,
                                IntPtr.Zero,
                                "SHELLDLL_DefView",
                                IntPtr.Zero);

    if (p != IntPtr.Zero)
    {
        workerw = p;
        workerws = W32.FindWindowEx(IntPtr.Zero,
                                   tophandle,
                                   "WorkerW",
                                   IntPtr.Zero);
    }

    return true;
}), IntPtr.Zero);

//创建窗口实例
Form1 form = new Form1();
form.Load += new EventHandler((s, e) =>
            {
                /// 窗口大小设置为屏幕的分辨率
                //Screen.PrimaryScreen.Bounds.Width
                int SH = Screen.PrimaryScreen.Bounds.Height;
                int SW = Screen.PrimaryScreen.Bounds.Width;
                form.Width = SW;
                form.Height = SH;
                form.Left = 0;
                form.Top = 0;
                form.resetBrowerPanelSize();

                // currentDir是计算得到的exe文件所在的目录，
                // URL 是本地html文件的路径 格式为"file:///F:\\MyWallPaper\\test\\myPage.html"
                // URL 也可以是一个网页地址
                string URL = "file:///" + currentDir + "\\resource\\index.html";
                form.setBrowerURL(URL);
                
                
                // 这个函数设置窗口的父子关系，可以通过spy++软件查看
                //W32.SetParent(form.Handle, progman);
                W32.SetParent(form.Handle, workerws);
                
            });

            // Start the Application Loop for the Form.
            Application.Run(form);
        }




```





