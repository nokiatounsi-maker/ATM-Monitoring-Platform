## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-07-14 - [Optimized ATM collection iteration]
**Learning:** Using `ConcurrentDictionary.Values` to iterate over a collection in C# forces a snapshot of the entire collection, which allocates significant memory (~8KB for 1000 items).
**Action:** Replaced `.Values` with a `yield return` iterator that traverses the dictionary directly. This reduces allocations to near zero (64 bytes for the enumerator object) and minimizes GC pressure. Also sealed core classes (`Atm`, `AtmService`, `AtmController`) to enable JIT devirtualization.
