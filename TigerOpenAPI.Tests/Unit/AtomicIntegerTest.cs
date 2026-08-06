using NUnit.Framework;
using TigerOpenAPI.Common.Util;

namespace TigerOpenAPI.Tests.Unit
{
  /// <summary>
  /// Verifies the atomic int operations in <see cref="AtomicInteger"/>
  /// (declared in <c>TigerOpenAPI.Common.Util</c>).
  /// </summary>
  [TestFixture]
  public class AtomicIntegerTest
  {
    /// <summary>The single-arg constructor seeds the initial value.</summary>
    [Test]
    public void Constructor_WithInitialValue_SetsValue()
    {
      AtomicInteger ai = new AtomicInteger(42);

      Assert.That(ai.Get(), Is.EqualTo(42));
    }

    /// <summary>The default constructor starts the counter at zero.</summary>
    [Test]
    public void DefaultConstructor_StartsAtZero()
    {
      AtomicInteger ai = new AtomicInteger();

      Assert.That(ai.Get(), Is.EqualTo(0));
    }

    /// <summary>Set replaces the current value.</summary>
    [Test]
    public void Set_NewValue_ReplacesValue()
    {
      AtomicInteger ai = new AtomicInteger(1);
      ai.Set(99);

      Assert.That(ai.Get(), Is.EqualTo(99));
    }

    /// <summary>GetAndSet returns the previous value and stores the new one.</summary>
    [Test]
    public void GetAndSet_ReturnsOldValueAndStoresNew()
    {
      AtomicInteger ai = new AtomicInteger(10);
      int previous = ai.GetAndSet(20);

      Assert.That(previous, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(20));
    }

    /// <summary>CompareAndSet returns true and updates when the expectation matches.</summary>
    [Test]
    public void CompareAndSet_WhenExpectMatches_ReturnsTrueAndUpdates()
    {
      AtomicInteger ai = new AtomicInteger(5);
      bool result = ai.CompareAndSet(5, 7);

      Assert.That(result, Is.True);
      Assert.That(ai.Get(), Is.EqualTo(7));
    }

    /// <summary>CompareAndSet returns false and leaves the value unchanged when expectation differs.</summary>
    [Test]
    public void CompareAndSet_WhenExpectDiffers_ReturnsFalseAndKeepsValue()
    {
      AtomicInteger ai = new AtomicInteger(5);
      bool result = ai.CompareAndSet(6, 7);

      Assert.That(result, Is.False);
      Assert.That(ai.Get(), Is.EqualTo(5));
    }

    /// <summary>GetAndIncrement returns the old value then increments.</summary>
    [Test]
    public void GetAndIncrement_ReturnsOldAndIncrements()
    {
      AtomicInteger ai = new AtomicInteger(3);
      int previous = ai.GetAndIncrement();

      Assert.That(previous, Is.EqualTo(3));
      Assert.That(ai.Get(), Is.EqualTo(4));
    }

    /// <summary>GetAndDecrement returns the old value then decrements.</summary>
    [Test]
    public void GetAndDecrement_ReturnsOldAndDecrements()
    {
      AtomicInteger ai = new AtomicInteger(3);
      int previous = ai.GetAndDecrement();

      Assert.That(previous, Is.EqualTo(3));
      Assert.That(ai.Get(), Is.EqualTo(2));
    }

    /// <summary>GetAndAdd returns the old value then adds the delta.</summary>
    [Test]
    public void GetAndAdd_ReturnsOldAndAddsDelta()
    {
      AtomicInteger ai = new AtomicInteger(8);
      int previous = ai.GetAndAdd(5);

      Assert.That(previous, Is.EqualTo(8));
      Assert.That(ai.Get(), Is.EqualTo(13));
    }

    /// <summary>GetAndAdd supports a negative delta.</summary>
    [Test]
    public void GetAndAdd_NegativeDelta_ReturnsOldAndSubtracts()
    {
      AtomicInteger ai = new AtomicInteger(10);
      int previous = ai.GetAndAdd(-4);

      Assert.That(previous, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(6));
    }

    /// <summary>IncrementAndGet increments then returns the new value.</summary>
    [Test]
    public void IncrementAndGet_ReturnsIncremented()
    {
      AtomicInteger ai = new AtomicInteger(0);
      int current = ai.IncrementAndGet();

      Assert.That(current, Is.EqualTo(1));
      Assert.That(ai.Get(), Is.EqualTo(1));
    }

    /// <summary>DecrementAndGet decrements then returns the new value.</summary>
    [Test]
    public void DecrementAndGet_ReturnsDecremented()
    {
      AtomicInteger ai = new AtomicInteger(0);
      int current = ai.DecrementAndGet();

      Assert.That(current, Is.EqualTo(-1));
      Assert.That(ai.Get(), Is.EqualTo(-1));
    }

    /// <summary>AddAndGet adds the delta then returns the new value.</summary>
    [Test]
    public void AddAndGet_ReturnsSum()
    {
      AtomicInteger ai = new AtomicInteger(7);
      int current = ai.AddAndGet(3);

      Assert.That(current, Is.EqualTo(10));
      Assert.That(ai.Get(), Is.EqualTo(10));
    }

    /// <summary>ToString returns the string representation of the current value.</summary>
    [Test]
    public void ToString_ReturnsStringValue()
    {
      AtomicInteger ai = new AtomicInteger(123);

      Assert.That(ai.ToString(), Is.EqualTo("123"));
    }
  }
}
