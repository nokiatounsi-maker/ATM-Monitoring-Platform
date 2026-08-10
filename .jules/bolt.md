## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-08-10 - [Avoiding ConcurrentDictionary.Values Snapshot Allocations]
**Learning:** Accessing the `.Values` property on a `ConcurrentDictionary` forces the collection to take a full snapshot under the hood, leading to significant memory allocations (approx 8 KB for 1,000 items). Iterating the dictionary directly or via a custom `yield return` iterator bypasses snapshot copy generation, reducing the allocation to just 64 bytes.
**Action:** Always iterate `ConcurrentDictionary` directly instead of querying `.Values` when returning or processing collections to minimize Gen0 GC pressure and memory allocations.
