## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-08-16 - [Direct ConcurrentDictionary Enumeration vs .Values Snapshot]
**Learning:** In .NET, accessing `ConcurrentDictionary.Values` creates a full snapshot of the values collection, allocating ~8 KB on the heap for 1000 items and triggering Gen0 GC collections. Iterating the dictionary directly via `foreach` / `yield return` avoids creating a snapshot array/collection, reducing heap allocations by ~98.6% (from ~8080 B to 112 B).
**Action:** When returning or enumerating values from `ConcurrentDictionary`, iterate the key-value pairs directly rather than invoking `.Values`.
