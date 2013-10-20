# Baser

## Overview

Baser is a set of common basic utilities and data structures, in both regular .net as well as ScriptSharp flavored .net.  The output of this project is used in downstream projects such as Facer.

Note that parts of Baser are designed to handle and abstract cross compilation across several environments: classic .net, WinRT .net, formerly Silverlight, and others.  You can see per-platform abstractions via platform specific file names (e.g. .ms.cs for Modern-style app specific code.) 

Common Features
	
 * Serializable Type Structures.  A serializable type system with compatible equivalents in Script.
 * Rich Document Basic Data Structures. Basic classes to establish rich content.  This will likely get moved out in the future.
 * Data Store.  Simple data structures that abstract data stores.
 * Basic Logging Utilities.  An event-driven logging system with platform-specific adapters for logging.

## Dependencies

Before compiling, make sure you run updateversions.cmd, which will execute VersionWriter (see Tooler project) to output _gen\verassemblyinfo.cs.

## License

Copyright (c) 2013, Bendyline LLC. Baser is licensed under the Apache 2.0 License.