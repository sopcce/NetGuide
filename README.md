<!-- TOC -->autoauto- [1. NetGuide介绍](#1-netguide介绍)auto- [2. C# 基础知识](#2-c-基础知识)auto    - [2.1. 组件介绍](#21-组件介绍)auto        - [2.1.1. Excel 操作](#211-excel-操作)auto            - [2.1.1.1. [NPOI](https://github.com/dotnetcore/NPOI)介绍](#2111-npoihttpsgithubcomdotnetcorenpoi介绍)auto            - [2.1.1.2. [NPOI](https://github.com/dotnetcore/NPOI)介绍](#2112-npoihttpsgithubcomdotnetcorenpoi介绍)auto    - [2.2. 特此感谢](#22-特此感谢)autoauto<!-- /TOC -->





# 1. NetGuide介绍
> 中文介绍为主，附带英文提交代码，做自己职业生涯的导航标。
为了优化大家的阅读体验，我重新进行了排版，并且增加了较为详细的目录供大家参考！如果有老哥对操作系统比较重要的知识总结过的话，欢迎找我哦！  
 

[![QQ群](https://img.shields.io/badge/QQ%E7%BE%A4-721420150-red.svg)](http://qm.qq.com/cgi-bin/qm/qr?k=zjBZ-kGA-LmVNuwPLPD8Xa5dtqli9EeY)
 
# 2. C# 基础知识


## 2.1. 组件介绍

### 2.1.1. Excel 操作
#### 2.1.1.1. [NPOI](https://github.com/dotnetcore/NPOI)介绍
> 用于读取和写入Microsoft Office二进制和OOXML文件格式的.NET库。
 
**详细代码如下**

Export Excel
```csharp
var newFile = @"newbook.core.xlsx";

using (var fs = new FileStream(newFile, FileMode.Create, FileAccess.Write)) {

    IWorkbook workbook = new XSSFWorkbook();

    ISheet sheet1 = workbook.CreateSheet("Sheet1");

    sheet1.AddMergedRegion(new CellRangeAddress(0, 0, 0, 10));
    var rowIndex = 0;
    IRow row = sheet1.CreateRow(rowIndex);
    row.Height = 30 * 80;
    row.CreateCell(0).SetCellValue("this is content");
    sheet1.AutoSizeColumn(0);
    rowIndex++;

    var sheet2 = workbook.CreateSheet("Sheet2");
    var style1 = workbook.CreateCellStyle();
    style1.FillForegroundColor = HSSFColor.Blue.Index2;
    style1.FillPattern = FillPattern.SolidForeground;

    var style2 = workbook.CreateCellStyle();
    style2.FillForegroundColor = HSSFColor.Yellow.Index2;
    style2.FillPattern = FillPattern.SolidForeground;

    var cell2 = sheet2.CreateRow(0).CreateCell(0);
    cell2.CellStyle = style1;
    cell2.SetCellValue(0);

    cell2 = sheet2.CreateRow(1).CreateCell(0);
    cell2.CellStyle = style2;
    cell2.SetCellValue(1);

    workbook.Write(fs);
}
```
Export Word
```csharp
 var newFile2 = @"newbook.core.docx";
using (var fs = new FileStream(newFile2, FileMode.Create, FileAccess.Write)) {
    XWPFDocument doc = new XWPFDocument();
    var p0 = doc.CreateParagraph();
    p0.Alignment = ParagraphAlignment.CENTER;
    XWPFRun r0 = p0.CreateRun();
    r0.FontFamily = "microsoft yahei";
    r0.FontSize = 18;
    r0.IsBold = true;
    r0.SetText("This is title");

    var p1 = doc.CreateParagraph();
    p1.Alignment = ParagraphAlignment.LEFT;
    p1.IndentationFirstLine = 500;
    XWPFRun r1 = p1.CreateRun();
    r1.FontFamily = "·ÂËÎ";
    r1.FontSize = 12;
    r1.IsBold = true;
    r1.SetText("This is content, content content content content content content content content content");

    doc.Write(fs);
}
```

----
#### 2.1.1.2. [NPOI](https://github.com/dotnetcore/NPOI)介绍

 
----
## 2.2. 特此感谢
- 参考资料项目  [JavaGuide](https://github.com/Snailclimb/JavaGuide)  
- 参考开源项目  [dotnet](https://docs.microsoft.com/zh-cn/dotnet/index)    
 -----
 
