using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Disque.Xna.Cameras;
using Disque.Xna.Primatives;

namespace Disque.Xna
{
    public class ImportModel : IPrimitive
    {
        public Texture2D Texture2D { get; set; }
        Model Model;
        Matrix[] modeltransforms;
        GraphicsDevice GraphicDevice;
        ContentManager Content;
        BoundingSphere sphere;
        bool boundingimplemented = false;
        public ImportModel(Model model, GraphicsDevice gd, ContentManager cm)
        {
            GraphicDevice = gd;
            Content = cm;
            Model = model;
            modeltransforms = new Matrix[Model.Bones.Count];
            Model.CopyAbsoluteBoneTransformsTo(modeltransforms);
            generateTags();
        }
        public void Draw(GameTime gt, Camera camera)
        {
            Matrix baseworld = Transformation;
            foreach (ModelMesh mesh in Model.Meshes)
            {
                Matrix localworld = modeltransforms[mesh.ParentBone.Index] * baseworld;
                foreach (ModelMeshPart meshpart in mesh.MeshParts)
                {
                    Effect effect = meshpart.Effect;
                    if (effect is BasicEffect)
                    {
                        ((BasicEffect)effect).World = localworld;
                        ((BasicEffect)effect).View = camera.View;
                        ((BasicEffect)effect).Projection = camera.Projection;
                        ((BasicEffect)effect).EnableDefaultLighting();
                        if (Texture2D != null)
                        {
                            ((BasicEffect)effect).Texture = Texture2D;
                        }
                    }
                    else
                    {
                        setEffectParameter(effect, "World", localworld);
                        setEffectParameter(effect, "View", camera.View);
                        setEffectParameter(effect, "Projection", camera.Projection);
                    }
                }
                mesh.Draw();
            }
        }
        public void CacheEffects()
        {
            foreach (ModelMesh mesh in Model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    ((MeshTag)part.Tag).CachedEffect = part.Effect;
        }
        public void RestoreEffects()
        {
            foreach (ModelMesh mesh in Model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    if (((MeshTag)part.Tag).CachedEffect != null)
                        part.Effect = ((MeshTag)part.Tag).CachedEffect;
        }
        public void SetModelEffect(Effect effect, bool CopyEffect)
        {
            foreach (ModelMesh mesh in Model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    Effect toSet = effect;
                    if (CopyEffect)
                        toSet = effect.Clone();
                    MeshTag tag = ((MeshTag)part.Tag);
                    if (tag.Texture != null)
                    {
                        setEffectParameter(toSet, "BasicTexture", tag.Texture);
                        setEffectParameter(toSet, "TextureEnabled", true);
                    }
                    else
                        setEffectParameter(toSet, "TextureEnabled", false);
                    // Set our remaining parameters to the effect
                    setEffectParameter(toSet, "DiffuseColor", tag.Color);
                    setEffectParameter(toSet, "SpecularPower", tag.SpecularPower);
                    part.Effect = toSet;
                }
        }
        void setEffectParameter(Effect effect, string paramName, object val)
        {
            if (effect.Parameters[paramName] == null)
                return;
            if (val is Vector3)
                effect.Parameters[paramName].SetValue((Vector3)val);
            else if (val is bool)
                effect.Parameters[paramName].SetValue((bool)val);
            else if (val is Matrix)
                effect.Parameters[paramName].SetValue((Matrix)val);
            else if (val is Texture2D)
                effect.Parameters[paramName].SetValue((Texture2D)val);
        }
        private void generateTags()
        {
            foreach (ModelMesh mesh in Model.Meshes)
                foreach (ModelMeshPart part in mesh.MeshParts)
                    if (part.Effect is BasicEffect)
                    {
                        BasicEffect effect = (BasicEffect)part.Effect;
                        MeshTag tag = new MeshTag(effect.DiffuseColor, effect.Texture,
                        effect.SpecularPower);
                        part.Tag = tag;
                    }
        }
        public BoundingSphere BoundingSphere
        {
            get
            {
                if (!boundingimplemented)
                {
                    foreach (ModelMesh mesh in Model.Meshes)
                    {
                        BoundingSphere transformed = mesh.BoundingSphere.Transform(
                        modeltransforms[mesh.ParentBone.Index]);
                        sphere = BoundingSphere.CreateMerged(sphere, transformed);
                    }
                    Matrix worldTransform = Transformation;
                    BoundingSphere transforme = sphere;
                    transforme = transforme.Transform(worldTransform);
                    return transforme;
                }
                else
                {
                    Matrix worldTransform = Transformation;
                    BoundingSphere transformed = sphere;
                    transformed = transformed.Transform(worldTransform);
                    return transformed;
                }
            }
        }
        public Matrix Transformation
        {
            get;
            set;
        }
        public void Update(GameTime gametime)
        {
        }
        public BoundingBox BoundingBox
        {
            get { throw new NotImplementedException(); }
        }
    }
}
