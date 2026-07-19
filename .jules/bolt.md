## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-07-19 - [Avoid ConcurrentDictionary.Values Allocations]
**Learning:** Accessing the `Values` property on `ConcurrentDictionary` takes a snapshot of the entire dictionary's values under a lock, resulting in O(N) memory allocations (over 8 KB for 1,000 items). Direct iteration or `yield return` iteration of the dictionary's `KeyValuePair` entries runs with zero snapshot overhead, reducing allocations to just 64 bytes (99.2% memory reduction).
**Action:** Avoid `.Values` property access in collections retrieved under hot paths or high-frequency API endpoints, utilizing direct iteration instead.
