## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2026-08-01 - [Avoid ConcurrentDictionary.Values Snapshot Allocation]
**Learning:** Accessing `ConcurrentDictionary.Values` takes a collection snapshot which allocates heap memory proportional to the size of the dictionary (O(N)). In contrast, iterating the dictionary directly via a foreach loop over the dictionary or a custom `yield return` iterator allocates constant memory (O(1)). In our benchmark with N=1000 items, direct iteration reduced allocation from 8,080 B to 112 B (a 98.6% reduction in allocated memory).
**Action:** Always prefer direct dictionary iteration or stream results using a `yield return` iterator instead of exposing `.Values` when returning or iterating over the collection in performance-critical paths.
