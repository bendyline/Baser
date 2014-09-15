# Baser

A set of general helper libraries for .net (on the front-end), and ScriptSharp 
on the JS side.

### Overview

Baser is a set of common basic utilities and data structures, in both regular 
.net as well as ScriptSharp flavored .net.  The output of this project is used 
in downstream projects such as Facer.

Note that parts of Baser are designed to handle and abstract cross compilation 
across several environments: classic .net, WinRT .net, and others.  You can see 
per-platform abstractions via platform specific file names 
(e.g. .ms.cs for Modern-style app specific code.) 

Common Features
	
 * _Serializable Type Structures._  A serializable type system with compatible 
equivalents in Script.

 * _Rich Document Basic Data Structures._ Basic classes to establish rich 
content.  This will likely get moved out in the future.

 * _Data Store._  Simple data structures that abstract data stores.

 * _Basic Logging Utilities._  An event-driven logging system with platform-
specific adapters for logging.

### Dependencies

Before compiling, make sure you run updateversions.cmd, which will execute 
VersionWriter (see the [Tooler project](http://github.com/bendyline/tooler)) to 
output _gen\verassemblyinfo.cs which contains standard version information.

### License

Copyright (c) 2014, Bendyline LLC. Baser is licensed under the Apache 2.0 
License.