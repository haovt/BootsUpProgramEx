using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpBasicAndAdvanced
{
    public class GenerateText : IAsyncEnumerable<int>
    {
        public async IAsyncEnumerator<int> GetAsyncEnumerator(CancellationToken cancellationToken = default)
        {
            for (int i = 0; i < 15; i++)
            {
                await Task.Delay(500);
                yield return i;
            }
        }
    }
}
