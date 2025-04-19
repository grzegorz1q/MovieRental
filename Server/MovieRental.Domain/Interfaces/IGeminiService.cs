using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieRental.Domain.Interfaces
{
    public interface IGeminiService
    {
        Task<string> AskGemini(string prompt);
    }
}
