## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-05-24 - [Optimization of dictionary enumeration]
**Learning:** Accessing `ConcurrentDictionary.Values` creates a full snapshot of the values in the dictionary, resulting in O(N) memory allocation. For large collections, this increases GC pressure.
**Action:** Replaced `_atms.Values` with a `yield return` iterator over the dictionary. Benchmarks for 1000 items showed a 98.6% reduction in memory allocation (from 8080 B to 112 B). Additionally, sealed core classes to enable JIT devirtualization.
