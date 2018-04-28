﻿// MonoGame - Copyright (C) The MonoGame Team
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using static WebHelper;
using static Retyped.dom;

namespace Microsoft.Xna.Framework.Graphics
{
    // ARB_framebuffer_object implementation
    partial class GraphicsDevice
    {
        internal class FramebufferHelper
        {
            private static FramebufferHelper _instance;

            public static FramebufferHelper Create(GraphicsDevice gd)
            {
                if (gd.GraphicsCapabilities.SupportsFramebufferObjectARB || gd.GraphicsCapabilities.SupportsFramebufferObjectEXT)
                {
                    _instance = new FramebufferHelper(gd);
                }
                else
                {
                    throw new PlatformNotSupportedException(
                        "MonoGame requires either ARB_framebuffer_object or EXT_framebuffer_object." +
                        "Try updating your graphics drivers.");
                }

                return _instance;
            }

            public static FramebufferHelper Get()
            {
                if (_instance == null)
                    throw new InvalidOperationException("The FramebufferHelper has not been created yet!");
                return _instance;
            }

            public bool SupportsInvalidateFramebuffer { get; private set; }

            public bool SupportsBlitFramebuffer { get; private set; }

            internal FramebufferHelper(GraphicsDevice graphicsDevice)
            {
                this.SupportsBlitFramebuffer = true;
                this.SupportsInvalidateFramebuffer = true;
            }

            internal virtual void GenRenderbuffer(out WebGLRenderbuffer renderbuffer)
            {
                renderbuffer = gl.createRenderbuffer();
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BindRenderbuffer(WebGLRenderbuffer renderbuffer)
            {
                gl.bindRenderbuffer(gl.RENDERBUFFER, renderbuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void DeleteRenderbuffer(WebGLRenderbuffer renderbuffer)
            {
                gl.deleteRenderbuffer(renderbuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void RenderbufferStorageMultisample(int samples, int internalFormat, int width, int height)
            {
                gl.renderbufferStorage(gl.RENDERBUFFER, internalFormat, width, height);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void GenFramebuffer(out WebGLFramebuffer framebuffer)
            {
                framebuffer = gl.createFramebuffer();
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BindFramebuffer(WebGLFramebuffer framebuffer)
            {
                gl.bindFramebuffer(gl.FRAMEBUFFER, framebuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void BindReadFramebuffer(int readFramebuffer)
            {
                // Requires WEBGL 2
                throw new NotImplementedException();
            }

            internal virtual void InvalidateDrawFramebuffer()
            {
                throw new NotImplementedException();
            }

            internal virtual void InvalidateReadFramebuffer()
            {
                throw new NotImplementedException();
            }

            internal virtual void DeleteFramebuffer(int framebuffer)
            {
                throw new NotImplementedException();
            }

            internal virtual void FramebufferTexture2D(int attachement, int target, WebGLTexture texture, int level = 0, int samples = 0)
            {
                gl.framebufferTexture2D(gl.FRAMEBUFFER, attachement, target, texture, level);
                GraphicsExtensions.CheckGLError();
            }

            /*internal virtual void FramebufferRenderbuffer(int attachement, int renderbuffer, int level = 0)
            {
                GL.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, (FramebufferAttachment)attachement, RenderbufferTarget.Renderbuffer, renderbuffer);
                GraphicsExtensions.CheckGLError();
            }

            internal virtual void GenerateMipmap(int target)
            {
                GL.GenerateMipmap((GenerateMipmapTarget)target);
                GraphicsExtensions.CheckGLError();

            }

            internal virtual void BlitFramebuffer(int iColorAttachment, int width, int height)
            {

                GL.ReadBuffer(ReadBufferMode.ColorAttachment0 + iColorAttachment);
                GraphicsExtensions.CheckGLError();
                GL.DrawBuffer(DrawBufferMode.ColorAttachment0 + iColorAttachment);
                GraphicsExtensions.CheckGLError();
                GL.BlitFramebuffer(0, 0, width, height, 0, 0, width, height, ClearBufferMask.ColorBufferBit, BlitFramebufferFilter.Nearest);
                GraphicsExtensions.CheckGLError();

            }

            internal virtual void CheckFramebufferStatus()
            {
                var status = GL.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
                if (status != FramebufferErrorCode.FramebufferComplete)
                {
                    string message = "Framebuffer Incomplete.";
                    switch (status)
                    {
                        case FramebufferErrorCode.FramebufferIncompleteAttachment: message = "Not all framebuffer attachment points are framebuffer attachment complete."; break;
                        case FramebufferErrorCode.FramebufferIncompleteMissingAttachment: message = "No images are attached to the framebuffer."; break;
                        case FramebufferErrorCode.FramebufferUnsupported: message = "The combination of internal formats of the attached images violates an implementation-dependent set of restrictions."; break;
                        case FramebufferErrorCode.FramebufferIncompleteMultisample: message = "Not all attached images have the same number of samples."; break;
                    }
                    throw new InvalidOperationException(message);
                }
            }*/
        }
    }
}
