using System;

namespace Core.DependencyInjection {
	[AttributeUsage(AttributeTargets.Field)]
	public class InjectAttribute: Attribute {
	}
}