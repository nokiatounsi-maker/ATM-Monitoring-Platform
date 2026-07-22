## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-05-24 - [ConcurrentDictionary Memory Optimization]
**Learning:** In the `AtmService.GetAllAtms()`, using the `ConcurrentDictionary.Values` property to return all items creates a full snapshot of the values list, allocating significant memory (~8080 bytes for 1000 items) and consuming extra garbage collection overhead.
**Action:** Refactored `GetAllAtms()` to iterate directly over the `ConcurrentDictionary` and yield each item. This reduces memory allocation by ~98.6% (from 8080 B down to 112 B), while sealing core classes (`Atm`, `AtmService`, `AtmController`) allows the .NET JIT compiler to perform devirtualization optimizations.
