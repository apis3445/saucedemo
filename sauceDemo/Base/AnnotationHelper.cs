using System.Collections.Generic;

namespace sauceDemo.Base
{
	public class AnnotationHelper
	{
		private List<Annotation> _annotations;

		public AnnotationHelper()
		{
			_annotations = new List<Annotation>();
		}

        public void AddAnnotation(AnnotationType annotationType, string description)
        {
			Annotation annotation = new Annotation();
			annotation.Description = description;
			annotation.AnnotationType = annotationType;
			this._annotations.Add(annotation);
        }
    }
}

