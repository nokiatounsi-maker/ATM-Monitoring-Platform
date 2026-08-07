## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-08-07 - [Bypassing Reflection in Controller Serialization]
**Learning:** ASP.NET Core MVC controllers by default rely on reflection-based JSON serialization (System.Text.Json) which causes CPU overhead and high Gen0 allocations on every HTTP request under load. Additionally, wrapping collection returns in OkObjectResult allocates a wrapper object on every call.
**Action:** Implemented System.Text.Json Source Generation via a partial JsonSerializerContext (AtmJsonContext) to resolve serialization metadata at compile time. Configured MVC's TypeInfoResolverChain to prioritize the compiled context. Refactored the controller endpoint to return IEnumerable<Atm> directly, bypassing OkObjectResult allocations.

## 2026-08-06 - [Avoiding ConcurrentDictionary.Values snapshotted allocation]
**Learning:** Accessing the `.Values` property of a `ConcurrentDictionary` takes a lock and creates a full snapshot collection of elements, allocating ~8 KB of memory for 1000 items. Direct iteration or `yield return` iterator bypasses this allocation entirely, reducing memory allocation by over 98% (~112 B).
**Action:** Enumerate `ConcurrentDictionary` directly using `foreach` or a custom generator returning `yield return` for web API responses rather than calling `.Values`.
