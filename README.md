# Concurrency Utilities

.NET Concurrency Utilities

Available utilities:

* VolatileLong
* VolatileDouble
* AtomicLong
* PaddedAtomicLong (padded version for atomic long to avoid false sharing)
* StripedLongAdder ( port of java.util.concurrent.atomic.LongAdder originaly written by Doug Lea)
* Striped64 ( port of java.util.concurrent.atomic.Striped64  originaly written by Doug Lea)
* ThreadLocalAdder ( similar with StripedLongAdder using ThreadLocal<> )

This utilities have been introduced in the [Metrics.NET](https://github.com/etishor/Metrics.NET) library, but can be useful for other projects also.

## Install

To minimize number of dependencies, this library is published on NuGet as source files.



## License

This library is release under Apache 2.0 License ( see LICENSE ) 
Copyright (c) 2015 Iulian Margarintescu