## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-08-20 - [Avoid ConcurrentDictionary.Values snapshot allocations]
**Learning:** Accessing `ConcurrentDictionary.Values` allocates a full collection snapshot (~8 KB for 1000 items) on every call. Direct iteration using `yield return` iterates key-value pairs without snapshot allocations (~112 B), reducing memory allocation by ~98.6%.
**Action:** When enumerating values from a `ConcurrentDictionary` in hot paths or API endpoints, iterate the dictionary directly with `yield return` or `foreach` rather than `.Values`.
