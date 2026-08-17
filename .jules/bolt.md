## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2025-05-23 - [System.Text.Json Source Generation and Devirtualization Optimization]
**Learning:** In ASP.NET Core APIs, default reflection-based JSON serialization causes unnecessary runtime memory allocations and CPU overhead during request/response handling. Using System.Text.Json Source Generation via `JsonSerializerContext` pre-generates serialization metadata at compile time, eliminating reflection overhead and lowering GC pressure. Additionally, sealing core classes (`Atm`, `AtmService`, `AtmController`) allows the JIT compiler to devirtualize method calls.
**Action:** Always configure `TypeInfoResolverChain` with a custom `JsonSerializerContext` for Web APIs to reduce allocations and improve throughput. Seal core implementation classes where inheritance is not required.
