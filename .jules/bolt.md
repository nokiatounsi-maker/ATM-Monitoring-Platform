## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2025-05-15 - [Optimization of ConcurrentDictionary iteration]
**Learning:** Accessing `ConcurrentDictionary.Values` for iteration triggers a full collection snapshot, leading to O(N) memory allocations (~8KB for 1000 items). While faster in terms of raw CPU time, it puts unnecessary pressure on the GC.
**Action:** Replaced `_atms.Values` with a `yield return` iterator over the dictionary. This reduces allocations from ~8KB to ~112B (the size of the iterator state machine) for 1000 items, at the cost of slightly higher CPU time, which is an acceptable trade-off in this high-throughput API.
