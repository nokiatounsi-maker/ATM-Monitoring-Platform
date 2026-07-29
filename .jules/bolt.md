## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-07-29 - [Avoid ConcurrentDictionary.Values Snapshot Allocations]
**Learning:** Calling `.Values` on a `ConcurrentDictionary` is a heavy operation that creates a full snapshot of the dictionary, allocating an internal list/array of size O(N). For large collections, this results in significant garbage collection (GC) pressure and overhead.
**Action:** Iterate the `ConcurrentDictionary` directly using a `foreach` loop and `yield return` to retrieve values. Benchmarks verified a 98.6% memory reduction (from 8080 B down to 112 B for 1000 items) and eliminated GC Gen 0 collections.

## 2026-07-29 - [Resolve .NET 10 OpenAPI Analyzer Compatibility under .NET 8.0 Target]
**Learning:** Under .NET 8.0 target framework, referencing the v10.0.3 package of `Microsoft.AspNetCore.OpenApi` can result in compilation failures because the generated source code references .NET 10 types not present in .NET 8.0.
**Action:** Instead of downgrading package versions (which can violate package preservation rules), dynamically filter out the conflicting OpenAPI Source Generator analyzer (`Microsoft.AspNetCore.OpenApi.SourceGenerators`) using an MSBuild target `DisableCompileTimeOpenApiXmlGenerator` before compilation.
