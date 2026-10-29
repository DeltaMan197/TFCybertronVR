# Contributing to TFCybertronVR

Thank you for your interest in contributing to the Transformers War For Cybertron VR mod!

## Code of Conduct

By contributing to this project, you agree to follow our Code of Conduct.

## How to Contribute

### Bug Reports

When reporting a bug, please include:

1. **Description**: Clear explanation of the issue
2. **Steps to Reproduce**: Detailed steps to reproduce the bug
3. **Expected Behavior**: What you expected to happen
4. **Actual Behavior**: What actually happened
5. **Environment**: VR headset, game version, OS
6. **Screenshots/Video**: If applicable

### Feature Requests

We welcome feature requests! Please include:

1. **Problem**: What problem are you trying to solve?
2. **Solution**: Proposed solution or feature
3. **Use Case**: How this would be used in practice
4. **Alternatives**: Any alternative solutions considered

### Code Contributions

#### Development Setup

```bash
# Clone the repository
git clone https://github.com/DeltaMan197/TFCybertronVR.git

# Checkout a feature branch
git checkout -b feature/your-feature-name

# Make your changes
# ...

# Test thoroughly
# ...

# Commit with descriptive messages
git commit -m "Add your feature description"

# Push to GitHub
git push origin feature/your-feature-name

# Create a Pull Request
```

#### Coding Standards

- **Naming**: Use PascalCase for classes, camelCase for variables
- **Comments**: Add XML documentation for public methods
- **Architecture**: Follow the TF2VR pattern where applicable
- **Testing**: Include unit tests for new features

### Pull Request Process

1. **Fork** the repository
2. **Clone** your fork
3. **Create** a branch from `main`
4. **Make** your changes
5. **Test** your changes thoroughly
6. **Commit** with clear, descriptive messages
7. **Push** to your fork
8. **Submit** a Pull Request

#### PR Checklist

- [ ] Code follows project style guide
- [ ] All changes are relevant to the feature/bug
- [ ] Documentation is updated (if applicable)
- [ ] Tests are added (if applicable)
- [ ] No unnecessary whitespace changes

### Architecture Decisions

Significant architectural changes should be discussed in Issues first via the **RFC (Request for Comments)** process:

1. Create an RFC issue
2. Discuss the proposal
3. Wait for community feedback (minimum 7 days)
4. Implement if approved

## File Structure Contributions

### Adding New Plugins

When adding a new C# plugin, follow this structure:

```
src/
└── NewPlugin.cs
```

Each plugin should:
- Be contained in a namespace (e.g., `TFCybertronVR.NewFeature`)
- Include proper XML documentation
- Handle exceptions gracefully
- Log using the existing Logger utility

### Modifying Existing Plugins

- Preserve original functionality
- Document changes in code comments
- Test thoroughly before submitting

## Documentation Contributions

### Types of Documentation

- **User Guides**: How-to instructions
- **API References**: Plugin developer guides
- **Tutorials**: Step-by-step walkthroughs
- **FAQs**: Common questions and answers

### Documentation Style

- Use Markdown for formatting
- Include code blocks with syntax highlighting
- Add cross-references to related sections
- Include version-specific notes where applicable

## Community Guidelines

### Be Respectful

- Be patient and constructive
- Accept constructive criticism
- Assume positive intent
- Focus on what is best for the community

### Be Welcoming

- Welcome newcomers
- Help others learn
- Encourage participation
- Report harassment promptly

### Be Helpful

- Offer assistance when possible
- Share knowledge
- Document your contributions