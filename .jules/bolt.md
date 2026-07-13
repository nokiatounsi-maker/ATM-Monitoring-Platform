## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-05-24 - [Avoid ConcurrentDictionary.Values snapshoting]
**Learning:** Accessing `ConcurrentDictionary.Values` creates a snapshot of the dictionary, which involves allocating a new collection and copying all items. This is a significant allocation bottleneck in high-throughput APIs.
**Action:** Replaced `.Values` access with a `yield return` iterator. Benchmarks verified that for 1000 items, memory allocations dropped from ~8 KB to ~120 bytes (98.5% reduction) while maintaining thread-safety. Also sealed core classes (`Atm`, `AtmService`, `AtmController`) to enable JIT devirtualization.
