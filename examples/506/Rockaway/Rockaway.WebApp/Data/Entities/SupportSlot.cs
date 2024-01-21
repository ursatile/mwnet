using System.Linq.Expressions;
using Microsoft.AspNetCore.Razor.Language;
using static System.Runtime.InteropServices.JavaScript.JSType;
using String = System.String;

namespace Rockaway.WebApp.Data.Entities;

public class SupportSlot {
	public Show Show { get; set; } = default!;
	public int SlotNumber { get; set; }
	public Artist Artist { get; set; } = default!;

	public static Action<EntityTypeBuilder<SupportSlot>> EntityModel => entity => {
		entity.HasKey(
			slot => slot.Show.Venue.Id,
			slot => slot.Show.Date,
			slot => slot.SlotNumber
		);
	};
}

public static class EntityTypeBuilderExtensions {

	private static string GetPropertyName<TEntity>(Expression<Func<TEntity, object?>> expr) {
		if (expr.Body is not UnaryExpression ux) throw new ArgumentException("Only member expressions are supported");
		return GetPropertyName<TEntity>(ux.Operand as MemberExpression);
	}

	private static string GetPropertyName<TEntity>(MemberExpression? mx)
		=> ((mx?.Expression is MemberExpression mex) ? GetPropertyName<TEntity>(mex) : String.Empty)
			+ mx?.Member.Name ?? throw new ArgumentException("Only member expressions are supported", nameof(mx));

	public static KeyBuilder<TEntity> HasKey<TEntity>(this EntityTypeBuilder<TEntity> builder,
		params Expression<Func<TEntity, object?>>[] keyExpressions) where TEntity : class =>
		builder.HasKey(keyExpressions.Select(GetPropertyName).ToArray());
}