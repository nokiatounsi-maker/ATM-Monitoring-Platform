## 2024-05-23 - [Optimization of ATM lookup]
**Learning:** In the `AtmService`, retrieving an ATM by its ID was using a `List<Atm>` with `FirstOrDefault(a => a.Id == id)`, resulting in O(N) complexity. For a large number of ATMs, this becomes a performance bottleneck in the API.
**Action:** Replaced the internal `List<Atm>` with a `ConcurrentDictionary<string, Atm>`. This reduces the lookup complexity to O(1) and ensures thread-safety for status updates. Benchmarks showed a 99% reduction in lookup time for N=1000 (from ~14.4 us to ~25 ns).

## 2025-05-24 - [Avoid ConcurrentDictionary.Values snapshot allocations]
**Learning:** Accessing `ConcurrentDictionary.Values` allocates an internal snapshot array every time it is called. Using `yield return` to iterate directly over the dictionary's key-value pairs avoids array snapshot allocations (~8 KB saved per call for 1000 items).
**Action:** Iterate `ConcurrentDictionary` directly using `yield return kvp.Value` when enumerating values without taking a collection snapshot.

## 2025-05-25 - [System.Text.Json Source Generation for API Controllers]
**Learning:** Default System.Text.Json serialization relies on runtime reflection, causing CPU overhead and metadata allocations during API request/response processing. Defining a `JsonSerializerContext` with `[JsonSerializable]` attributes and attaching it to `TypeInfoResolverChain` in ASP.NET Core eliminates runtime reflection overhead and reduces garbage collection pressure.
**Action:** Register source-generated `JsonSerializerContext` instances in `options.JsonSerializerOptions.TypeInfoResolverChain` for API models.
