/// <reference path="../../Scripts/typings/angularjs/angular.d.ts"/>
/// <reference path="../../Scripts/typings/snapsvg/snapsvg.d.ts"/>

'use strict';

/* Directives */

module OhSnap.CreateFracture.Directive {

    var createFractureDirectives: angular.IModule = angular.module(
        'CreateFractureDirectives',
        [
            //'ngSanitize',
            //'ui.select'
        ]);

    // This decorates a specially-prepared SVG by adding invisible clickable rectangles to it. 
    // 
    // It works like this. The SVG must contain a layer called "click-layer". 
    // Generally it'll be the top layer and it'll be empty. This directive then
    // looks for every rect in the SVG with an ID of the pattern "ao-label-NNNN".
    // For each of these, it creates a new rect in the click layer at the exact
    // same XY area. It makes this rect clickable, and when one of these rects
    // is clicked it calls "$scope.aoPrefixClicked(NNNN)", where the NNN 
    // parameter comes from the original rect's ID. 
    //
    // On other words, it creates invisible clickables rects over existing rects,
    // and when these rects are clicked they call a function in the caller's
    // scope, allow the caller to respond to clicks.
    //
    // The SVG element itself must have the ID "ao-skeleton-image".
    //
    // The whole point is to avoid having to maintain invisible, perfectly-aligned 
    // rectangles in an SVG.
    //
    // To use this directive, do something like this:
    //     <fracreg-skeleton-image>
    //         <object type="image/svg+xml" id= "ao-skeleton-image" data= "skeleton.svg">< /object>
    //     < /fracreg-skeleton-image>
    createFractureDirectives.directive(
        'fracregSkeletonImage',
        [() => {
            function link(scope, element, attrs) {
                var that = this;

                $(element).find('#ao-skeleton-image')[0].addEventListener(
                    'load',
                    (event) => {
                        var handle = Snap('#ao-skeleton-image');
                        var clickLayer: Snap.Paper = <Snap.Paper>handle.select('g#click-layer');
                        var rects = [];
                        handle.selectAll('rect').forEach(
                            (r) => { rects.push(r); }
                            );
                        rects = _.filter(
                            rects,
                            (r) => {
                                return r.node.id.search('ao-label-') == 0;
                            });

                        _.each(rects,
                            (r) => {
                                // Extract the AO code from the rect's ID
                                var ao_code_prefix = r.node.id.substring(9, 11);

                                // Make the new, clickable rect.
                                var bbox = r.getBBox();
                                var clickable = clickLayer.rect(
                                    bbox.x,
                                    bbox.y,
                                    bbox.width,
                                    bbox.height);

                                clickable.attr({
                                    fill: "#000000",
                                    opacity: 0,
                                    id: 'ao-clickable-' + ao_code_prefix
                                });

                                // Have the new rect call the scope function on click.
                                clickable.click(
                                    (evt) => {
                                        scope.aoPrefixClicked(ao_code_prefix);                                        
                                    });
                            });
                    });
            }

            return {
                link: link
            };
        }]);

}
