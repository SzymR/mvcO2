using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Services
{
    public static class GalleryImages
    {
        // definicja rozmiarów aby dodac nowa wielkosc wystarczy dodac linie new ImageDimensions("medium",400,400)
        public static readonly ReadOnlyCollection<ImageDimensions> GalleryDimensionsList = new ReadOnlyCollection<ImageDimensions>
        (new[] {
            new ImageDimensions("large",900,600),
            new ImageDimensions("small",200,200)
        });
    }
}