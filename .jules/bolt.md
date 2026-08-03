## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-06-15 - [Direct iteration vs ConcurrentDictionary.Values snapshot]
**Learning:** Accessing `ConcurrentDictionary.Values` generates a snapshot list under the hood, allocating memory proportional to the size of the dictionary (e.g., ~8 KB for 1000 elements). Directly iterating over the ConcurrentDictionary using key-value pairs or a yield-return iterator avoids copying and snapshot allocations, achieving a 99% reduction in heap memory allocation.
**Action:** Always prefer direct iteration or a custom yield-return iterator when returning elements from a ConcurrentDictionary over returning `.Values` to minimize GC pressure and heap allocations.
