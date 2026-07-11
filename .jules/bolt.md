## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-07-11 - [Reduced allocations in ConcurrentDictionary iteration]
**Learning:** Accessing `ConcurrentDictionary.Values` creates a snapshot by copying all values into a new collection, leading to unnecessary memory allocations (O(N) space). Iterating the dictionary directly via `foreach` (yielding `KeyValuePair`) avoids this allocation.
**Action:** Refactored `AtmService.GetAllAtms` to iterate the dictionary directly and `yield return` values. Benchmarks confirmed that for 1000 items, memory allocation for the iteration dropped from ~8 KB to 64 bytes.

## 2024-07-11 - [Sealing classes for JIT optimization]
**Learning:** In .NET, sealing classes (especially those in hot paths like controllers and services) enables JIT devirtualization, reducing the overhead of virtual calls and enabling further optimizations.
**Action:** Added the `sealed` keyword to `Atm`, `AtmService`, and `AtmController`.
