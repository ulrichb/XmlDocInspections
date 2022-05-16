# XML Doc Inspections ReSharper Extension

[![Build status](https://github.com/ulrichb/XmlDocInspections/actions/workflows/build.yml/badge.svg)](https://github.com/ulrichb/XmlDocInspections/actions/workflows/build.yml)
[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/ulrichb/XmlDocInspections?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)
<img src="https://dl.dropbox.com/s/8p9d03ycoy4nf06/master-linecoverage.svg" alt="Line Coverage" title="Line Coverage">
<img src="https://dl.dropbox.com/s/ywhaxs30rto3ezm/master-branchcoverage.svg" alt="Branch Coverage" title="Branch Coverage">

Plugin pages: [ReSharper](https://plugins.jetbrains.com/plugin/11648-xml-doc-inspections) / [Rider](https://plugins.jetbrains.com/plugin/10151-xml-doc-inspections)

[History of changes](History.md)

## Description

A simple ReSharper extension which enables to show warnings for missing XML Doc comments for types and type members. In contrast to the C# compiler's [CS1591 warning](https://msdn.microsoft.com/en-us/library/zk18c1w9.aspx) which emits a warning for all public or protected types and members, this extension provides configuration options. Further it comes with a [quick fix](#quick-fix) action to generate doc comment templates.

### ReSharper options page

<img src="/Doc/OptionsPage.png" alt="Options Page" width="780" />

### Quick Fix

<img src="/Doc/QuickFix.gif" alt="QuickFix" width="520" />
