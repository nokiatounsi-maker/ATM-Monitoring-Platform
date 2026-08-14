## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2024-08-14 - [ConcurrentDictionary Snapshot Overhead and Source-Generated Serialization]
**Learning:** Returning `ConcurrentDictionary.Values` creates an array-based snapshot of the collection under the hood, causing high GC allocation overhead. Iterating the dictionary directly via `foreach` over the dictionary itself or yielding avoids this snapshot. Furthermore, default reflection-based JSON serialization in ASP.NET Core causes significant allocation and runtime overhead, which can be avoided entirely using System.Text.Json Source Generation.
**Action:** Used `yield return` to iterate `ConcurrentDictionary` directly, return results as un-wrapped `IEnumerable<T>` from controller actions to bypass `OkObjectResult` heap overhead, and configured `AppJsonSerializerContext` to eliminate reflection serialization costs.
