using Newtonsoft.Json;

namespace OES;

/// <summary>
/// A request to create a new <see cref="ScriptSlicingDefinition"/>.
/// </summary>
public class CreateScriptSlicingDefinition
{
    public CreateScriptSlicingDefinition(
        int         scriptDefinitionId,
        int         page,
        ImageMargin range,
        int?        panelId,
        int?        orderInPanel,
        int?        linkedSliceId,
        int?        orderInLinkage,
        int?        linkedQuestionNumberBoxId
        )
    {
        ScriptDefinitionId = scriptDefinitionId;
        Page               = page;
        Range              = range;
        _panelId           = panelId;
        OrderInPanel       = orderInPanel;
        _linkedSliceId     = linkedSliceId;
        OrderInLinkage     = orderInLinkage;
        _linkedQnbId       = linkedQuestionNumberBoxId;
    }
    
    /// <inheritdoc cref="ScriptSlicingDefinition.ScriptDefinitionId"/>
    public int ScriptDefinitionId { get; set; }
    
    /// <inheritdoc cref="ScriptSlicingDefinition.Page"/>
    public int Page { get; set; }
    
    /// <inheritdoc cref="ScriptSlicingDefinition.Range"/>
    public ImageMargin Range { get; set; }

    /// <summary>
    /// The panel which is responsible for marking the slice.
    /// Setting this property will make <see cref="LinkedSliceId"/>, <see cref="OrderInLinkage"/>,
    /// and <see cref="LinkedQuestionNumberBoxId"/> null.
    /// </summary>
    public int? PanelId
    {
        get => _panelId;
        set
        {
            _panelId = value;
            
            if (_panelId is null) return;
            _linkedSliceId = null;
            OrderInLinkage = null;
            _linkedQnbId   = null;
        }
    }
    private int? _panelId;

    /// <inheritdoc cref="ScriptSlicingDefinition.OrderInPanel"/>
    public int? OrderInPanel { get; set; }

    /// <summary>
    /// The ID of the slice to which this slice is linked.
    /// Setting this property will make <see cref="PanelId"/>, <see cref="OrderInPanel"/>
    /// and <see cref="LinkedQuestionNumberBoxId"/> null.
    /// </summary>
    [JsonProperty("link_to_slice")]
    public int? LinkedSliceId
    {
        get => _linkedSliceId;
        set
        {
            _linkedSliceId = value;

            if (_linkedSliceId is null) return;
            _panelId       = null;
            OrderInPanel   = null;
            _linkedQnbId   = null;
        }
    }
    private int? _linkedSliceId;
    
    /// <inheritdoc cref="ScriptSlicingDefinition.OrderInLinkage"/>
    [JsonProperty("link_order")]
    public int? OrderInLinkage { get; set; }
    
    /// <summary>
    /// The ID of the Question Number Box that this slice is linked to.
    /// Setting this property will make <see cref="PanelId"/>, <see cref="OrderInPanel"/>,
    /// <see cref="LinkedSliceId"/>, and <see cref="OrderInLinkage"/> null.
    /// </summary>
    [JsonProperty("link_to_qnb")]
    public int? LinkedQuestionNumberBoxId
    {
        get => _linkedQnbId;
        set
        {
            _linkedQnbId = value;

            if (_linkedQnbId is null) return;
            _panelId       = null;
            OrderInPanel   = null;
            _linkedSliceId = null;
            OrderInLinkage = null;
        }
    }
    private int? _linkedQnbId;
}