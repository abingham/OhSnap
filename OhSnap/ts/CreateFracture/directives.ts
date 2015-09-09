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
                                var ao_code_prefix = r.node.id.substring(9, 11);

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
