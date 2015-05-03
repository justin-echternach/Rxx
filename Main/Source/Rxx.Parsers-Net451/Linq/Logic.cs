﻿using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Rxx.Parsers.Linq
{
  public static partial class Parser
  {
    /// <summary>
    /// Matches the specified <paramref name="parser"/> when the specified <paramref name="notParser"/> does not match.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TNotResult">The type of the elements that are generated by the <paramref name="notParser"/>.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser to match when the specified <paramref name="notParser"/> does not match.</param>
    /// <param name="notParser">The parser that when it matches will cause the matches from the specified 
    /// <paramref name="parser"/> to be ignored.</param>
    /// <returns>A parser that matches the specified <paramref name="parser"/> when the specified <paramref name="notParser"/>
    /// does not match.</returns>
    public static IParser<TSource, TResult> Not<TSource, TNotResult, TResult>(
      this IParser<TSource, TResult> parser,
      IParser<TSource, TNotResult> notParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(notParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, TResult>>() != null);

      return notParser.None(default(TNotResult)).SelectMany(_ => parser, (_, result) => result);
    }

    /// <summary>
    /// Matches either the left parser or the right parser.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser that has precedence.</param>
    /// <param name="nextParser">The parser that is matched if the first <paramref name="parser"/> does not match.</param>
    /// <returns>A parser that yields the matches from <paramref name="parser"/> if there are any; otherwise, the matches 
    /// from <paramref name="nextParser"/> are yielded.</returns>
    public static IParser<TSource, TResult> Or<TSource, TResult>(
      this IParser<TSource, TResult> parser,
      IParser<TSource, TResult> nextParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(nextParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, TResult>>() != null);

      // The following code is an optimization that combines nested Ors into a single Or.
      // Without this optimization, Or could be defined simply as Any(new[] { parser, nextParser }).

      var left = new List<IParser<TSource, TResult>>();
      var right = new List<IParser<TSource, TResult>>();

      var nestedLeft = parser as AnyParser<TSource, TResult>;
      var nestedRight = nextParser as AnyParser<TSource, TResult>;

      if (nestedLeft == null)
      {
        left.Add(parser);
      }
      else
      {
        left.AddRange(nestedLeft.Parsers);
      }

      if (nestedRight == null)
      {
        right.Add(nextParser);
      }
      else
      {
        right.AddRange(nestedRight.Parsers);
      }

      return Any(left.Concat(right));
    }

    /// <summary>
    /// Matches the first parser that is successful.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The enumerable sequence of parsers to be matched until one is successful.</param>
    /// <returns>A parser that yields the matches from the first parser in <paramref name="parsers"/> that is successful.</returns>
    public static IParser<TSource, TResult> Any<TSource, TResult>(
      this IEnumerable<IParser<TSource, TResult>> parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, TResult>>() != null);

      return new AnyParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches the first parser that is successful.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The array of parsers to be matched until one is successful.</param>
    /// <returns>A parser that yields the matches from the first parser in <paramref name="parsers"/> that is successful.</returns>
    public static IParser<TSource, TResult> Any<TSource, TResult>(
      params IParser<TSource, TResult>[] parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, TResult>>() != null);

      return new AnyParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches the left parser followed by the right parser and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser to be matched first.</param>
    /// <param name="nextParser">The parser to be matched after the first <paramref name="parser"/>.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser followed by the second parser, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> And<TSource, TResult>(
      this IParser<TSource, TResult> parser,
      IParser<TSource, TResult> nextParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(nextParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // There is no need for optimization here because And always produces IEnumerable<TResult>, thus the
      // compiler will choose a different overload of And when nested.  However, All must be called so that
      // optimization may work on this result within the other All overloads because it relies on a specific
      // type returned by All.

      return All(new[] { parser, nextParser });
    }

    /// <summary>
    /// Matches the left parser followed by the right parser and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser to be matched first.</param>
    /// <param name="nextParser">The parser to be matched after the first <paramref name="parser"/>.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser followed by the second parser, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> And<TSource, TResult>(
      this IParser<TSource, IEnumerable<TResult>> parser,
      IParser<TSource, TResult> nextParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(nextParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // The following code is an optimization that combines nested Ands (on the left side) into a single And.
      // It's not possible for this overload to be called with a nested And on the right side because And always
      // produces IEnumerable<TResult> and this overload defines the right side as a scalar TResult, thus the 
      // compiler will choose a different overload of And when nested on the right.

      var left = new List<IParser<TSource, TResult>>();
      var leftMany = new List<IParser<TSource, IEnumerable<TResult>>>();
      var nestedLeft = parser as AllParser<TSource, TResult>;
      var nestedLeftMany = parser as AllManyParser<TSource, TResult>;

      if (nestedLeft == null && nestedLeftMany == null)
      {
        leftMany.Add(parser);
      }
      else if (nestedLeft != null)
      {
        left.AddRange(nestedLeft.Parsers);
      }
      else
      {
        leftMany.AddRange(nestedLeftMany.Parsers);
      }

      // If the left or right side has no parsers then the parse operation must fail, but must be evaluated anyway.
      // e.g., Given the nested combination of someParser.And(All(emptyParserList)), this is gauranteed to fail 
      // on All, so it seems futile to even attempt parsing the left side; however, since there may be side-effects 
      // the left side must be parsed anyway.  There are several nested parser combinations that meet this criteria.
      // Futhermore, all combinations must not throw an exception.
      // In any possible case where the left or right side doesn't have any parsers, the parse operation must 
      // result in a parse failure (i.e., no results) - not an exception.

      if (left.Count > 0)
      {
        return All(left.Concat(new[] { nextParser }));
      }
      else
      {
        return All(leftMany.Concat(new[] { nextParser.Amplify() }));
      }
    }

    /// <summary>
    /// Matches the left parser followed by the right parser and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser to be matched first.</param>
    /// <param name="nextParser">The parser to be matched after the first <paramref name="parser"/>.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser followed by the second parser, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> And<TSource, TResult>(
      this IParser<TSource, TResult> parser,
      IParser<TSource, IEnumerable<TResult>> nextParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(nextParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // The following code is an optimization that combines nested Ands (on the right side) into a single And.
      // It's not possible for this overload to be called with a nested And on the left side because And always
      // produces IEnumerable<TResult> and this overload defines the left side as a scalar TResult, thus the 
      // compiler will choose a different overload of And when nested on the left.

      var right = new List<IParser<TSource, TResult>>();
      var rightMany = new List<IParser<TSource, IEnumerable<TResult>>>();
      var nestedRight = nextParser as AllParser<TSource, TResult>;
      var nestedRightMany = nextParser as AllManyParser<TSource, TResult>;

      if (nestedRight == null && nestedRightMany == null)
      {
        rightMany.Add(nextParser);
      }
      else if (nestedRight != null)
      {
        right.AddRange(nestedRight.Parsers);
      }
      else
      {
        rightMany.AddRange(nestedRightMany.Parsers);
      }

      // If the left or right side has no parsers then the parse operation must fail, but must be evaluated anyway.
      // e.g., Given the nested combination of someParser.And(All(emptyParserList)), this is gauranteed to fail 
      // on All, so it seems futile to even attempt parsing the left side; however, since there may be side-effects 
      // the left side must be parsed anyway.  There are several nested parser combinations that meet this criteria.
      // Futhermore, all combinations must not throw an exception.
      // In any possible case where the left or right side doesn't have any parsers, the parse operation must 
      // result in a parse failure (i.e., no results) - not an exception.

      if (right.Count > 0)
      {
        return All(new[] { parser }.Concat(right));
      }
      else
      {
        return All(new[] { parser.Amplify() }.Concat(rightMany));
      }
    }

    /// <summary>
    /// Matches the left parser followed by the right parser and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">The parser to be matched first.</param>
    /// <param name="nextParser">The parser to be matched after the first <paramref name="parser"/>.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser followed by the second parser, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> And<TSource, TResult>(
      this IParser<TSource, IEnumerable<TResult>> parser,
      IParser<TSource, IEnumerable<TResult>> nextParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(nextParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      var left = new List<IParser<TSource, TResult>>();
      var right = new List<IParser<TSource, TResult>>();

      var leftMany = new List<IParser<TSource, IEnumerable<TResult>>>();
      var rightMany = new List<IParser<TSource, IEnumerable<TResult>>>();

      var nestedLeft = parser as AllParser<TSource, TResult>;
      var nestedRight = nextParser as AllParser<TSource, TResult>;

      var nestedLeftMany = parser as AllManyParser<TSource, TResult>;
      var nestedRightMany = nextParser as AllManyParser<TSource, TResult>;

      if (nestedLeft == null && nestedLeftMany == null)
      {
        leftMany.Add(parser);
      }
      else if (nestedLeft != null)
      {
        left.AddRange(nestedLeft.Parsers);
      }
      else
      {
        leftMany.AddRange(nestedLeftMany.Parsers);
      }

      if (nestedRight == null && nestedRightMany == null)
      {
        rightMany.Add(nextParser);
      }
      else if (nestedRight != null)
      {
        right.AddRange(nestedRight.Parsers);
      }
      else
      {
        rightMany.AddRange(nestedRightMany.Parsers);
      }

      // If the left or right side has no parsers then the parse operation must fail, but must be evaluated anyway.
      // e.g., Given the nested combination of someParser.And(All(emptyParserList)), this is gauranteed to fail 
      // on All, so it seems futile to even attempt parsing the left side; however, since there may be side-effects 
      // the left side must be parsed anyway.  There are several nested parser combinations that meet this criteria.
      // Futhermore, all combinations must not throw an exception.
      // In any possible case where the left or right side doesn't have any parsers, the parse operation must 
      // result in a parse failure (i.e., no results) - not an exception.

      if (left.Count > 0)
      {
        if (right.Count > 0)
        {
          return All(left.Concat(right));
        }
        else
        {
          return All(left.Select(p => p.Amplify()).Concat(rightMany));
        }
      }
      else if (right.Count > 0)
      {
        return All(leftMany.Concat(right.Select(p => p.Amplify())));
      }
      else
      {
        return All(leftMany.Concat(rightMany));
      }
    }

    /// <summary>
    /// Matches the left parser and the right parser in any order and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">A parser to be matched, before or after the other parser.</param>
    /// <param name="otherParser">The other parser to be matched, before or after the first parser.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser and the second parser in any order, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AndUnordered<TSource, TResult>(
      this IParser<TSource, TResult> parser,
      IParser<TSource, TResult> otherParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(otherParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // Optimization similar to And is inappropriate here because a grammar of sequential AndUnordered parsers actually implies 
      // an order that must be respected between each unordered group.  For example, a parser grammar such as "{12}{34}", where 
      // each character within {} is combined using AndUnordered, must match any of the following input sequences: "1234", "2134", 
      // "1243", "2143"; however, it must not match any of the following input sequences: "3124", "4321", "3241", "4123", etc.

      return AllUnordered(new[] { parser, otherParser });
    }

    /// <summary>
    /// Matches the left parser and the right parser in any order and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">A parser to be matched, before or after the other parser.</param>
    /// <param name="otherParser">The other parser to be matched, before or after the first parser.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser and the second parser in any order, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AndUnordered<TSource, TResult>(
      this IParser<TSource, IEnumerable<TResult>> parser,
      IParser<TSource, TResult> otherParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(otherParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // Optimization similar to And is inappropriate here because a grammar of sequential AndUnordered parsers actually implies 
      // an order that must be respected between each unordered group.  For example, a parser grammar such as "{12}{34}", where 
      // each character within {} is combined using AndUnordered, must match any of the following input sequences: "1234", "2134", 
      // "1243", "2143"; however, it must not match any of the following input sequences: "3124", "4321", "3241", "4123", etc.

      return AllUnordered(new[] { parser, otherParser.Amplify() });
    }

    /// <summary>
    /// Matches the left parser and the right parser in any order and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">A parser to be matched, before or after the other parser.</param>
    /// <param name="otherParser">The other parser to be matched, before or after the first parser.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser and the second parser in any order, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AndUnordered<TSource, TResult>(
      this IParser<TSource, TResult> parser,
      IParser<TSource, IEnumerable<TResult>> otherParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(otherParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // Optimization similar to And is inappropriate here because a grammar of sequential AndUnordered parsers actually implies 
      // an order that must be respected between each unordered group.  For example, a parser grammar such as "{12}{34}", where 
      // each character within {} is combined using AndUnordered, must match any of the following input sequences: "1234", "2134", 
      // "1243", "2143"; however, it must not match any of the following input sequences: "3124", "4321", "3241", "4123", etc.

      return AllUnordered(new[] { parser.Amplify(), otherParser });
    }

    /// <summary>
    /// Matches the left parser and the right parser in any order and yields the matches from both in a concatenated sequence, 
    /// or yields no matches if either the first or second parser has no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parser">A parser to be matched, before or after the other parser.</param>
    /// <param name="otherParser">The other parser to be matched, before or after the first parser.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from the first parser and the second parser in any order, 
    /// or no matches if either the first or second parser has no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AndUnordered<TSource, TResult>(
      this IParser<TSource, IEnumerable<TResult>> parser,
      IParser<TSource, IEnumerable<TResult>> otherParser)
    {
      Contract.Requires(parser != null);
      Contract.Requires(otherParser != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      // Optimization similar to And is inappropriate here because a grammar of sequential AndUnordered parsers actually implies 
      // an order that must be respected between each unordered group.  For example, a parser grammar such as "{12}{34}", where 
      // each character within {} is combined using AndUnordered, must match any of the following input sequences: "1234", "2134", 
      // "1243", "2143"; however, it must not match any of the following input sequences: "3124", "4321", "3241", "4123", etc.

      return AllUnordered(new[] { parser, otherParser });
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in the specified order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The enumerable sequence of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/>, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> All<TSource, TResult>(
      this IEnumerable<IParser<TSource, TResult>> parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in the specified order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The array of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/>, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> All<TSource, TResult>(
      params IParser<TSource, TResult>[] parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in the specified order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements of the result sequences that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The enumerable sequence of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/>, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> All<TSource, TResult>(
      this IEnumerable<IParser<TSource, IEnumerable<TResult>>> parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllManyParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in the specified order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements of the result sequences that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The array of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/>, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> All<TSource, TResult>(
      params IParser<TSource, IEnumerable<TResult>>[] parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllManyParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in any order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The enumerable sequence of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/> in any order, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AllUnordered<TSource, TResult>(
      this IEnumerable<IParser<TSource, TResult>> parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllUnorderedParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in any order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The array of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/> in any order, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AllUnordered<TSource, TResult>(
      params IParser<TSource, TResult>[] parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllUnorderedParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in any order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements of the result sequences that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The enumerable sequence of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/> in any order, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AllUnordered<TSource, TResult>(
      this IEnumerable<IParser<TSource, IEnumerable<TResult>>> parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllManyUnorderedParser<TSource, TResult>(parsers);
    }

    /// <summary>
    /// Matches all <paramref name="parsers"/> in any order and yields the results in a concatenated sequence, 
    /// or yields no matches if any of the parsers have no matches.
    /// </summary>
    /// <typeparam name="TSource">The type of the source elements.</typeparam>
    /// <typeparam name="TResult">The type of the elements of the result sequences that are generated from parsing the source elements.</typeparam>
    /// <param name="parsers">The array of parsers to be matched.</param>
    /// <returns>A parser that yields a concatenated sequence of matches from all of the specified <paramref name="parsers"/> in any order, 
    /// or no matches if any of the parsers have no matches.</returns>
    public static IParser<TSource, IEnumerable<TResult>> AllUnordered<TSource, TResult>(
      params IParser<TSource, IEnumerable<TResult>>[] parsers)
    {
      Contract.Requires(parsers != null);
      Contract.Ensures(Contract.Result<IParser<TSource, IEnumerable<TResult>>>() != null);

      return new AllManyUnorderedParser<TSource, TResult>(parsers);
    }
  }
}