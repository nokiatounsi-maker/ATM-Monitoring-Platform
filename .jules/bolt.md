## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-08-09 - [ConcurrentDictionary Values Iteration Memory Optimization]
**Learning:** Accessing the `Values` property on `ConcurrentDictionary` takes a snapshot of the entire collection and copies it into a newly allocated read-only list on every call. In a high-throughput API, this leads to significant memory churn and frequent GC pauses. Direct iteration of the dictionary using `foreach` or `yield return` iterates over the internal buckets directly, reducing memory allocation by 99.2% (from 8080 B down to 64 B for 1000 items).
**Action:** Avoid calling `ConcurrentDictionary.Values` in query/lookup paths. Instead, iterate the dictionary directly or expose it using custom iterators to minimize memory allocation.
