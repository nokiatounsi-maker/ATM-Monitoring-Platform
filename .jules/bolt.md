## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-07-21 - [Optimization of ConcurrentDictionary retrieval]
**Learning:** Accessing the `.Values` property of a `ConcurrentDictionary` takes a lock/snapshot of the dictionary, allocating an O(N) helper collection/array on the heap (e.g. 8.08 KB for 1000 items). Directly iterating the dictionary and using a custom iterator ('yield return') reduces heap allocation to a tiny, constant 112 B regardless of the collection size (a 98.6% allocation reduction for N=1000).
**Action:** Always avoid `.Values` or `.Keys` on concurrent collection types when iterating. Instead, iterate the collection directly via foreach or a custom yield return generator to bypass collection snapshotting and minimize GC overhead.
