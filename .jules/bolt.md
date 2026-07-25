## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-07-25 - [Optimization of ATM status querying and list allocations]
**Learning:** Direct access of `_atms.Values` on a `ConcurrentDictionary` allocates a snapshot of the entire collection and locks all internal buckets under heavy contention. For large dictionaries, this creates a significant GC overhead. Additionally, the target framework `.net8.0` is incompatible with `Microsoft.AspNetCore.OpenApi` version 10.x, causing build errors due to missing .NET 9+ OpenApi interfaces.
**Action:** Replaced `.Values` with a deferred iterator using `yield return` and direct dictionary iteration to avoid snapshots, resulting in a 98.6% memory reduction for N=1000 items (112 B vs 8 KB). Downgraded OpenAPI packages to 8.0.0 / 6.6.2 to resolve compilation.
