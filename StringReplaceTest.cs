﻿using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Engines;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace csharp_benchmarks
{
    [SimpleJob(RunStrategy.Throughput, launchCount: 1)]
    public class StringReplaceTest
    {
        const string shortString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit ";

        //belive me is ~25k characters
        const string longString = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit efficitur est. Phasellus bibendum felis venenatis massa bibendum ornare. Quisque et eros cursus odio ultrices dictum. Morbi fringilla tortor et magna auctor, et volutpat ipsum fermentum. Praesent sed nibh ipsum. Praesent ut neque quis metus posuere semper non quis est. Nam rhoncus sem et magna pellentesque auctor. Proin dui elit, aliquet in lorem non, gravida sagittis quam. Nam ac dictum tellus. Nunc quis nulla leo. Aliquam interdum metus tincidunt, ultrices lacus eu, hendrerit sapien. Etiam vestibulum justo et urna ullamcorper laoreet. Nunc at augue ut ex interdum finibus. Phasellus sit amet mollis lacus, a consectetur nisi. Sed et nunc quam.Suspendisse rhoncus nec ante in tincidunt.Quisque a lacinia dolor.Proin auctor tortor purus, vitae facilisis ex vehicula ut.Aliquam sed viverra elit, at iaculis mauris.Sed vitae hendrerit ipsum. Mauris consequat, lorem vitae eleifend tempor, tellus dui tincidunt nulla, eget efficitur justo ipsum non enim. Aenean pulvinar convallis sem, ut ullamcorper ipsum cursus eget.Cras et rhoncus leo. Nulla porta sem nec ipsum dignissim eleifend.Nam semper consequat lorem, eget auctor nunc commodo ac.Nulla sit amet erat at tortor rutrum ultricies et ut ex.Sed id sagittis libero. Mauris aliquet leo vitae massa auctor, pretium consequat erat sodales.Nunc dapibus porta nulla, vitae scelerisque enim fermentum sed.Nulla facilisi. Pellentesque dignissim commodo blandit. Etiam eu purus a enim porttitor tristique sit amet sit amet arcu. Phasellus at mattis felis, molestie placerat sem.Mauris cursus quis eros ac ultrices. Nulla eu quam facilisis, dapibus nibh non, ultrices magna. Curabitur eros magna, venenatis non augue vel, sollicitudin porttitor ipsum.Nunc sit amet lectus nec nulla fringilla placerat. Sed nec vulputate sem. Suspendisse at diam eu urna ultricies egestas et eu risus. Duis ultricies erat a venenatis mollis. Fusce sagittis metus a est laoreet, ut vulputate diam porta. Donec pharetra ante nec ullamcorper tempus. Donec ultrices, diam ut ullamcorper vehicula, lectus turpis sodales est, in rutrum ligula dolor a nisl.Etiam eget lacinia nisi.Quisque sed eros lorem. Proin pellentesque erat a elit mattis volutpat.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Vestibulum vitae rhoncus ante, sed sagittis nibh.Aliquam aliquam sem elit, non porttitor risus venenatis ac.Maecenas ut luctus tortor. Curabitur leo nulla, imperdiet eget mattis auctor, mollis non dui.Quisque condimentum dapibus leo, in commodo lectus gravida quis. Curabitur tempus, est dapibus venenatis pretium, ante massa venenatis enim, id tincidunt mauris ante ut eros. Aenean mattis consectetur leo non sollicitudin. Donec maximus semper risus id auctor. Vivamus eu sapien viverra, vestibulum orci ac, gravida lectus. Pellentesque finibus arcu vitae turpis rhoncus, sit amet sollicitudin sem molestie.Proin eu massa in arcu mattis viverra.Phasellus tempor volutpat sem. Sed sed eros consectetur, congue est et, commodo leo.Cras cursus justo augue, sed ornare dolor sollicitudin quis.Vestibulum interdum efficitur dolor, eget maximus justo aliquet at.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Morbi id maximus lorem, ac ultricies arcu.Morbi eu scelerisque ante. Sed accumsan metus eget consequat venenatis. Morbi facilisis eu nulla feugiat luctus. Quisque aliquam laoreet ornare. Mauris quis felis in massa laoreet consectetur nec in purus.Ut nec dapibus libero. Aliquam fringilla lorem mattis purus pharetra porttitor.Pellentesque posuere suscipit libero, eget dignissim orci tristique a.Vestibulum dignissim ac felis quis condimentum. Morbi elementum cursus consectetur. Vivamus elementum, erat et porttitor sagittis, nisi erat interdum lacus, eu luctus elit velit at ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit efficitur est. Phasellus bibendum felis venenatis massa bibendum ornare. Quisque et eros cursus odio ultrices dictum. Morbi fringilla tortor et magna auctor, et volutpat ipsum fermentum. Praesent sed nibh ipsum. Praesent ut neque quis metus posuere semper non quis est. Nam rhoncus sem et magna pellentesque auctor. Proin dui elit, aliquet in lorem non, gravida sagittis quam. Nam ac dictum tellus. Nunc quis nulla leo. Aliquam interdum metus tincidunt, ultrices lacus eu, hendrerit sapien. Etiam vestibulum justo et urna ullamcorper laoreet. Nunc at augue ut ex interdum finibus. Phasellus sit amet mollis lacus, a consectetur nisi. Sed et nunc quam.Suspendisse rhoncus nec ante in tincidunt.Quisque a lacinia dolor.Proin auctor tortor purus, vitae facilisis ex vehicula ut.Aliquam sed viverra elit, at iaculis mauris.Sed vitae hendrerit ipsum. Mauris consequat, lorem vitae eleifend tempor, tellus dui tincidunt nulla, eget efficitur justo ipsum non enim. Aenean pulvinar convallis sem, ut ullamcorper ipsum cursus eget.Cras et rhoncus leo. Nulla porta sem nec ipsum dignissim eleifend.Nam semper consequat lorem, eget auctor nunc commodo ac.Nulla sit amet erat at tortor rutrum ultricies et ut ex.Sed id sagittis libero. Mauris aliquet leo vitae massa auctor, pretium consequat erat sodales.Nunc dapibus porta nulla, vitae scelerisque enim fermentum sed.Nulla facilisi. Pellentesque dignissim commodo blandit. Etiam eu purus a enim porttitor tristique sit amet sit amet arcu. Phasellus at mattis felis, molestie placerat sem.Mauris cursus quis eros ac ultrices. Nulla eu quam facilisis, dapibus nibh non, ultrices magna. Curabitur eros magna, venenatis non augue vel, sollicitudin porttitor ipsum.Nunc sit amet lectus nec nulla fringilla placerat. Sed nec vulputate sem. Suspendisse at diam eu urna ultricies egestas et eu risus. Duis ultricies erat a venenatis mollis. Fusce sagittis metus a est laoreet, ut vulputate diam porta. Donec pharetra ante nec ullamcorper tempus. Donec ultrices, diam ut ullamcorper vehicula, lectus turpis sodales est, in rutrum ligula dolor a nisl.Etiam eget lacinia nisi.Quisque sed eros lorem. Proin pellentesque erat a elit mattis volutpat.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Vestibulum vitae rhoncus ante, sed sagittis nibh.Aliquam aliquam sem elit, non porttitor risus venenatis ac.Maecenas ut luctus tortor. Curabitur leo nulla, imperdiet eget mattis auctor, mollis non dui.Quisque condimentum dapibus leo, in commodo lectus gravida quis. Curabitur tempus, est dapibus venenatis pretium, ante massa venenatis enim, id tincidunt mauris ante ut eros. Aenean mattis consectetur leo non sollicitudin. Donec maximus semper risus id auctor. Vivamus eu sapien viverra, vestibulum orci ac, gravida lectus. Pellentesque finibus arcu vitae turpis rhoncus, sit amet sollicitudin sem molestie.Proin eu massa in arcu mattis viverra.Phasellus tempor volutpat sem. Sed sed eros consectetur, congue est et, commodo leo.Cras cursus justo augue, sed ornare dolor sollicitudin quis.Vestibulum interdum efficitur dolor, eget maximus justo aliquet at.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Morbi id maximus lorem, ac ultricies arcu.Morbi eu scelerisque ante. Sed accumsan metus eget consequat venenatis. Morbi facilisis eu nulla feugiat luctus. Quisque aliquam laoreet ornare. Mauris quis felis in massa laoreet consectetur nec in purus.Ut nec dapibus libero. Aliquam fringilla lorem mattis purus pharetra porttitor.Pellentesque posuere suscipit libero, eget dignissim orci tristique a.Vestibulum dignissim ac felis quis condimentum. Morbi elementum cursus consectetur. Vivamus elementum, erat et porttitor sagittis, nisi erat interdum lacus, eu luctus elit velit at ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit efficitur est. Phasellus bibendum felis venenatis massa bibendum ornare. Quisque et eros cursus odio ultrices dictum. Morbi fringilla tortor et magna auctor, et volutpat ipsum fermentum. Praesent sed nibh ipsum. Praesent ut neque quis metus posuere semper non quis est. Nam rhoncus sem et magna pellentesque auctor. Proin dui elit, aliquet in lorem non, gravida sagittis quam. Nam ac dictum tellus. Nunc quis nulla leo. Aliquam interdum metus tincidunt, ultrices lacus eu, hendrerit sapien. Etiam vestibulum justo et urna ullamcorper laoreet. Nunc at augue ut ex interdum finibus. Phasellus sit amet mollis lacus, a consectetur nisi. Sed et nunc quam.Suspendisse rhoncus nec ante in tincidunt.Quisque a lacinia dolor.Proin auctor tortor purus, vitae facilisis ex vehicula ut.Aliquam sed viverra elit, at iaculis mauris.Sed vitae hendrerit ipsum. Mauris consequat, lorem vitae eleifend tempor, tellus dui tincidunt nulla, eget efficitur justo ipsum non enim. Aenean pulvinar convallis sem, ut ullamcorper ipsum cursus eget.Cras et rhoncus leo. Nulla porta sem nec ipsum dignissim eleifend.Nam semper consequat lorem, eget auctor nunc commodo ac.Nulla sit amet erat at tortor rutrum ultricies et ut ex.Sed id sagittis libero. Mauris aliquet leo vitae massa auctor, pretium consequat erat sodales.Nunc dapibus porta nulla, vitae scelerisque enim fermentum sed.Nulla facilisi. Pellentesque dignissim commodo blandit. Etiam eu purus a enim porttitor tristique sit amet sit amet arcu. Phasellus at mattis felis, molestie placerat sem.Mauris cursus quis eros ac ultrices. Nulla eu quam facilisis, dapibus nibh non, ultrices magna. Curabitur eros magna, venenatis non augue vel, sollicitudin porttitor ipsum.Nunc sit amet lectus nec nulla fringilla placerat. Sed nec vulputate sem. Suspendisse at diam eu urna ultricies egestas et eu risus. Duis ultricies erat a venenatis mollis. Fusce sagittis metus a est laoreet, ut vulputate diam porta. Donec pharetra ante nec ullamcorper tempus. Donec ultrices, diam ut ullamcorper vehicula, lectus turpis sodales est, in rutrum ligula dolor a nisl.Etiam eget lacinia nisi.Quisque sed eros lorem. Proin pellentesque erat a elit mattis volutpat.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Vestibulum vitae rhoncus ante, sed sagittis nibh.Aliquam aliquam sem elit, non porttitor risus venenatis ac.Maecenas ut luctus tortor. Curabitur leo nulla, imperdiet eget mattis auctor, mollis non dui.Quisque condimentum dapibus leo, in commodo lectus gravida quis. Curabitur tempus, est dapibus venenatis pretium, ante massa venenatis enim, id tincidunt mauris ante ut eros. Aenean mattis consectetur leo non sollicitudin. Donec maximus semper risus id auctor. Vivamus eu sapien viverra, vestibulum orci ac, gravida lectus. Pellentesque finibus arcu vitae turpis rhoncus, sit amet sollicitudin sem molestie.Proin eu massa in arcu mattis viverra.Phasellus tempor volutpat sem. Sed sed eros consectetur, congue est et, commodo leo.Cras cursus justo augue, sed ornare dolor sollicitudin quis.Vestibulum interdum efficitur dolor, eget maximus justo aliquet at.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Morbi id maximus lorem, ac ultricies arcu.Morbi eu scelerisque ante. Sed accumsan metus eget consequat venenatis. Morbi facilisis eu nulla feugiat luctus. Quisque aliquam laoreet ornare. Mauris quis felis in massa laoreet consectetur nec in purus.Ut nec dapibus libero. Aliquam fringilla lorem mattis purus pharetra porttitor.Pellentesque posuere suscipit libero, eget dignissim orci tristique a.Vestibulum dignissim ac felis quis condimentum. Morbi elementum cursus consectetur. Vivamus elementum, erat et porttitor sagittis, nisi erat interdum lacus, eu luctus elit velit at ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit efficitur est. Phasellus bibendum felis venenatis massa bibendum ornare. Quisque et eros cursus odio ultrices dictum. Morbi fringilla tortor et magna auctor, et volutpat ipsum fermentum. Praesent sed nibh ipsum. Praesent ut neque quis metus posuere semper non quis est. Nam rhoncus sem et magna pellentesque auctor. Proin dui elit, aliquet in lorem non, gravida sagittis quam. Nam ac dictum tellus. Nunc quis nulla leo. Aliquam interdum metus tincidunt, ultrices lacus eu, hendrerit sapien. Etiam vestibulum justo et urna ullamcorper laoreet. Nunc at augue ut ex interdum finibus. Phasellus sit amet mollis lacus, a consectetur nisi. Sed et nunc quam.Suspendisse rhoncus nec ante in tincidunt.Quisque a lacinia dolor.Proin auctor tortor purus, vitae facilisis ex vehicula ut.Aliquam sed viverra elit, at iaculis mauris.Sed vitae hendrerit ipsum. Mauris consequat, lorem vitae eleifend tempor, tellus dui tincidunt nulla, eget efficitur justo ipsum non enim. Aenean pulvinar convallis sem, ut ullamcorper ipsum cursus eget.Cras et rhoncus leo. Nulla porta sem nec ipsum dignissim eleifend.Nam semper consequat lorem, eget auctor nunc commodo ac.Nulla sit amet erat at tortor rutrum ultricies et ut ex.Sed id sagittis libero. Mauris aliquet leo vitae massa auctor, pretium consequat erat sodales.Nunc dapibus porta nulla, vitae scelerisque enim fermentum sed.Nulla facilisi. Pellentesque dignissim commodo blandit. Etiam eu purus a enim porttitor tristique sit amet sit amet arcu. Phasellus at mattis felis, molestie placerat sem.Mauris cursus quis eros ac ultrices. Nulla eu quam facilisis, dapibus nibh non, ultrices magna. Curabitur eros magna, venenatis non augue vel, sollicitudin porttitor ipsum.Nunc sit amet lectus nec nulla fringilla placerat. Sed nec vulputate sem. Suspendisse at diam eu urna ultricies egestas et eu risus. Duis ultricies erat a venenatis mollis. Fusce sagittis metus a est laoreet, ut vulputate diam porta. Donec pharetra ante nec ullamcorper tempus. Donec ultrices, diam ut ullamcorper vehicula, lectus turpis sodales est, in rutrum ligula dolor a nisl.Etiam eget lacinia nisi.Quisque sed eros lorem. Proin pellentesque erat a elit mattis volutpat.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Vestibulum vitae rhoncus ante, sed sagittis nibh.Aliquam aliquam sem elit, non porttitor risus venenatis ac.Maecenas ut luctus tortor. Curabitur leo nulla, imperdiet eget mattis auctor, mollis non dui.Quisque condimentum dapibus leo, in commodo lectus gravida quis. Curabitur tempus, est dapibus venenatis pretium, ante massa venenatis enim, id tincidunt mauris ante ut eros. Aenean mattis consectetur leo non sollicitudin. Donec maximus semper risus id auctor. Vivamus eu sapien viverra, vestibulum orci ac, gravida lectus. Pellentesque finibus arcu vitae turpis rhoncus, sit amet sollicitudin sem molestie.Proin eu massa in arcu mattis viverra.Phasellus tempor volutpat sem. Sed sed eros consectetur, congue est et, commodo leo.Cras cursus justo augue, sed ornare dolor sollicitudin quis.Vestibulum interdum efficitur dolor, eget maximus justo aliquet at.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Morbi id maximus lorem, ac ultricies arcu.Morbi eu scelerisque ante. Sed accumsan metus eget consequat venenatis. Morbi facilisis eu nulla feugiat luctus. Quisque aliquam laoreet ornare. Mauris quis felis in massa laoreet consectetur nec in purus.Ut nec dapibus libero. Aliquam fringilla lorem mattis purus pharetra porttitor.Pellentesque posuere suscipit libero, eget dignissim orci tristique a.Vestibulum dignissim ac felis quis condimentum. Morbi elementum cursus consectetur. Vivamus elementum, erat et porttitor sagittis, nisi erat interdum lacus, eu luctus elit velit at ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit efficitur est. Phasellus bibendum felis venenatis massa bibendum ornare. Quisque et eros cursus odio ultrices dictum. Morbi fringilla tortor et magna auctor, et volutpat ipsum fermentum. Praesent sed nibh ipsum. Praesent ut neque quis metus posuere semper non quis est. Nam rhoncus sem et magna pellentesque auctor. Proin dui elit, aliquet in lorem non, gravida sagittis quam. Nam ac dictum tellus. Nunc quis nulla leo. Aliquam interdum metus tincidunt, ultrices lacus eu, hendrerit sapien. Etiam vestibulum justo et urna ullamcorper laoreet. Nunc at augue ut ex interdum finibus. Phasellus sit amet mollis lacus, a consectetur nisi. Sed et nunc quam.Suspendisse rhoncus nec ante in tincidunt.Quisque a lacinia dolor.Proin auctor tortor purus, vitae facilisis ex vehicula ut.Aliquam sed viverra elit, at iaculis mauris.Sed vitae hendrerit ipsum. Mauris consequat, lorem vitae eleifend tempor, tellus dui tincidunt nulla, eget efficitur justo ipsum non enim. Aenean pulvinar convallis sem, ut ullamcorper ipsum cursus eget.Cras et rhoncus leo. Nulla porta sem nec ipsum dignissim eleifend.Nam semper consequat lorem, eget auctor nunc commodo ac.Nulla sit amet erat at tortor rutrum ultricies et ut ex.Sed id sagittis libero. Mauris aliquet leo vitae massa auctor, pretium consequat erat sodales.Nunc dapibus porta nulla, vitae scelerisque enim fermentum sed.Nulla facilisi. Pellentesque dignissim commodo blandit. Etiam eu purus a enim porttitor tristique sit amet sit amet arcu. Phasellus at mattis felis, molestie placerat sem.Mauris cursus quis eros ac ultrices. Nulla eu quam facilisis, dapibus nibh non, ultrices magna. Curabitur eros magna, venenatis non augue vel, sollicitudin porttitor ipsum.Nunc sit amet lectus nec nulla fringilla placerat. Sed nec vulputate sem. Suspendisse at diam eu urna ultricies egestas et eu risus. Duis ultricies erat a venenatis mollis. Fusce sagittis metus a est laoreet, ut vulputate diam porta. Donec pharetra ante nec ullamcorper tempus. Donec ultrices, diam ut ullamcorper vehicula, lectus turpis sodales est, in rutrum ligula dolor a nisl.Etiam eget lacinia nisi.Quisque sed eros lorem. Proin pellentesque erat a elit mattis volutpat.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Vestibulum vitae rhoncus ante, sed sagittis nibh.Aliquam aliquam sem elit, non porttitor risus venenatis ac.Maecenas ut luctus tortor. Curabitur leo nulla, imperdiet eget mattis auctor, mollis non dui.Quisque condimentum dapibus leo, in commodo lectus gravida quis. Curabitur tempus, est dapibus venenatis pretium, ante massa venenatis enim, id tincidunt mauris ante ut eros. Aenean mattis consectetur leo non sollicitudin. Donec maximus semper risus id auctor. Vivamus eu sapien viverra, vestibulum orci ac, gravida lectus. Pellentesque finibus arcu vitae turpis rhoncus, sit amet sollicitudin sem molestie.Proin eu massa in arcu mattis viverra.Phasellus tempor volutpat sem. Sed sed eros consectetur, congue est et, commodo leo.Cras cursus justo augue, sed ornare dolor sollicitudin quis.Vestibulum interdum efficitur dolor, eget maximus justo aliquet at.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Morbi id maximus lorem, ac ultricies arcu.Morbi eu scelerisque ante. Sed accumsan metus eget consequat venenatis. Morbi facilisis eu nulla feugiat luctus. Quisque aliquam laoreet ornare. Mauris quis felis in massa laoreet consectetur nec in purus.Ut nec dapibus libero. Aliquam fringilla lorem mattis purus pharetra porttitor.Pellentesque posuere suscipit libero, eget dignissim orci tristique a.Vestibulum dignissim ac felis quis condimentum. Morbi elementum cursus consectetur. Vivamus elementum, erat et porttitor sagittis, nisi erat interdum lacus, eu luctus elit velit at ligula.Lorem ipsum dolor sit amet, consectetur adipiscing elit. Proin orci massa, gravida at felis ac, hendrerit efficitur est. Phasellus bibendum felis venenatis massa bibendum ornare. Quisque et eros cursus odio ultrices dictum. Morbi fringilla tortor et magna auctor, et volutpat ipsum fermentum. Praesent sed nibh ipsum. Praesent ut neque quis metus posuere semper non quis est. Nam rhoncus sem et magna pellentesque auctor. Proin dui elit, aliquet in lorem non, gravida sagittis quam. Nam ac dictum tellus. Nunc quis nulla leo. Aliquam interdum metus tincidunt, ultrices lacus eu, hendrerit sapien. Etiam vestibulum justo et urna ullamcorper laoreet. Nunc at augue ut ex interdum finibus. Phasellus sit amet mollis lacus, a consectetur nisi. Sed et nunc quam.Suspendisse rhoncus nec ante in tincidunt.Quisque a lacinia dolor.Proin auctor tortor purus, vitae facilisis ex vehicula ut.Aliquam sed viverra elit, at iaculis mauris.Sed vitae hendrerit ipsum. Mauris consequat, lorem vitae eleifend tempor, tellus dui tincidunt nulla, eget efficitur justo ipsum non enim. Aenean pulvinar convallis sem, ut ullamcorper ipsum cursus eget.Cras et rhoncus leo. Nulla porta sem nec ipsum dignissim eleifend.Nam semper consequat lorem, eget auctor nunc commodo ac.Nulla sit amet erat at tortor rutrum ultricies et ut ex.Sed id sagittis libero. Mauris aliquet leo vitae massa auctor, pretium consequat erat sodales.Nunc dapibus porta nulla, vitae scelerisque enim fermentum sed.Nulla facilisi. Pellentesque dignissim commodo blandit. Etiam eu purus a enim porttitor tristique sit amet sit amet arcu. Phasellus at mattis felis, molestie placerat sem.Mauris cursus quis eros ac ultrices. Nulla eu quam facilisis, dapibus nibh non, ultrices magna. Curabitur eros magna, venenatis non augue vel, sollicitudin porttitor ipsum.Nunc sit amet lectus nec nulla fringilla placerat. Sed nec vulputate sem. Suspendisse at diam eu urna ultricies egestas et eu risus. Duis ultricies erat a venenatis mollis. Fusce sagittis metus a est laoreet, ut vulputate diam porta. Donec pharetra ante nec ullamcorper tempus. Donec ultrices, diam ut ullamcorper vehicula, lectus turpis sodales est, in rutrum ligula dolor a nisl.Etiam eget lacinia nisi.Quisque sed eros lorem. Proin pellentesque erat a elit mattis volutpat.Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos.Vestibulum vitae rhoncus ante, sed sagittis nibh.Aliquam aliquam sem elit, non porttitor risus venenatis ac.Maecenas ut luctus tortor. Curabitur leo nulla, imperdiet eget mattis auctor, mollis non dui.Quisque condimentum dapibus leo, in commodo lectus gravida quis. Curabitur tempus, est dapibus venenatis pretium, ante massa venenatis enim, id tincidunt mauris ante ut eros. Aenean mattis consectetur leo non sollicitudin. Donec maximus semper risus id auctor. Vivamus eu sapien viverra, vestibulum orci ac, gravida lectus. Pellentesque finibus arcu vitae turpis rhoncus, sit amet sollicitudin sem molestie.Proin eu massa in arcu mattis viverra.Phasellus tempor volutpat sem. Sed sed eros consectetur, congue est et, commodo leo.Cras cursus justo augue, sed ornare dolor sollicitudin quis.Vestibulum interdum efficitur dolor, eget maximus justo aliquet at.Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas.Morbi id maximus lorem, ac ultricies arcu.Morbi eu scelerisque ante. Sed accumsan metus eget consequat venenatis. Morbi facilisis eu nulla feugiat luctus. Quisque aliquam laoreet ornare. Mauris quis felis in massa laoreet consectetur nec in purus.Ut nec dapibus libero. Aliquam fringilla lorem mattis purus pharetra porttitor.Pellentesque posuere suscipit libero, eget dignissim orci tristique a.Vestibulum dignissim ac felis quis condimentum. Morbi elementum cursus consectetur. Vivamus elementum, erat et porttitor sagittis, nisi erat interdum lacus, eu luctus elit velit at ligula.";

        [Benchmark]
        public string StringReplace() => (shortString.Replace("massa", "replaced massa").ToString());

        [Benchmark]
        public string StringBuilderReplace() => (new StringBuilder(shortString).Replace("massa", "replaced massa").ToString());

        [Benchmark]
        public string StringRegexReplace() => (Regex.Replace(shortString, "massa", "replaced massa").ToString());

        [Benchmark]
        public string LongStringReplace() => (longString.Replace("massa", "replaced massa").ToString());

        [Benchmark]
        public string LongStringBuilderReplace() => (new StringBuilder(longString).Replace("massa", "replaced massa").ToString());

        [Benchmark]
        public string LongStringRegexReplace() => (Regex.Replace(longString, "massa", "replaced massa").ToString());

        [Benchmark]
        public string LongStringRegexCompiledReplace() => (Regex.Replace(longString, "massa", "replaced massa", RegexOptions.Compiled | RegexOptions.IgnoreCase).ToString());

        [Benchmark]
        public string StringMultipleReplace() => (shortString.Replace("massa", "replaced massa").Replace("orci", "replaced orci").ToString());

        [Benchmark]
        public string StringBuilderMultipleReplace() => (new StringBuilder(shortString).Replace("massa", "replaced massa").Replace("orci", "replaced orci").ToString());

        [Benchmark]
        public string StringRegexMultipleReplace()
        {
            IDictionary<string, string> replacement = new Dictionary<string, string>()
            {
                    {"massa","replaced massa"},
                    {"orci","replaced orci"}
            };
            return new Regex(String.Join("|", replacement.Keys)).Replace(shortString, m => replacement[m.Value]);
        }

        [Benchmark]
        public string LongStringMultipleReplace() => (longString.Replace("massa", "replaced massa").Replace("orci", "replaced orci").ToString());

        [Benchmark]
        public string LongStringBuilderMultipleReplace() => (new StringBuilder(longString).Replace("massa", "replaced massa").Replace("orci", "replaced orci").ToString());

        [Benchmark]
        public string LongStringRegexMultipleReplace()
        {
            IDictionary<string, string> replacement = new Dictionary<string, string>()
            {
                    {"massa","replaced massa"},
                    {"orci","replaced orci"}
            };
            return new Regex(String.Join("|", replacement.Keys)).Replace(longString, m => replacement[m.Value]);
        }
    }
}