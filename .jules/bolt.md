## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-05-24 - [Avoid ConcurrentDictionary.Values Snapshotting and Enable JIT Devirtualization]
**Learning:** Accessing `ConcurrentDictionary.Values` in the API path results in a heap-allocated array/collection snapshot of all dictionary values, which scales linearly (O(N) allocation) and increases GC pressure. Furthermore, not sealing hot-path core classes like `Atm`, `AtmService`, and `AtmController` prevents the JIT compiler from performing devirtualization optimizations.
**Action:** Replaced the `.Values` access with a direct `yield return` iteration over the `ConcurrentDictionary`'s KeyValuePairs, which keeps memory allocation flat at O(1) (~112 B regardless of N). Sealed the core classes (`Atm`, `AtmService`, `AtmController`) to enable JIT devirtualization optimizations.
